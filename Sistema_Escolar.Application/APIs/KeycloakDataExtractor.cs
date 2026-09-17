using Amazon.Runtime;
using Microsoft.Extensions.Caching.Memory;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Sistema_Escolar.Application.APIs
{
    public class KeycloakDataExtractor
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private const string BaseUrl = "https://k3y.financialsoft.site";
        private const string Realm = "CREDITFS";
        private const string ClientId = "admin-cli-net";
        private const string ClientSecret = "MskVeYpB2GnNfZ7yldr2ojgHxYb2CQcH";

        private const string TOKEN_CACHE_KEY = "keycloak_admin_token";

        public KeycloakDataExtractor(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        private async Task<string> GetAccessToken()
        {
            if (_cache.TryGetValue(TOKEN_CACHE_KEY, out string? cachedToken) && !string.IsNullOrEmpty(cachedToken)) return cachedToken;

            var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var context = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", ClientId),
                new KeyValuePair<string, string>("client_secret", ClientSecret),
            });

            var tokenRes = await httpClient.PostAsync(
                $"{BaseUrl}/realms/{Realm}/protocol/openid-connect/token",
                context
            );

            tokenRes.EnsureSuccessStatusCode();

            var tokenJson = await tokenRes.Content.ReadAsStringAsync();
            var tokenDoc = JsonDocument.Parse(tokenJson);

            var accessToken = tokenDoc.RootElement.GetProperty("access_token").GetString()!;
            var expiresIn = tokenDoc.RootElement.GetProperty("expires_in").GetInt32();

            var cacheExpiration = TimeSpan.FromSeconds(expiresIn * 0.8);
            _cache.Set(TOKEN_CACHE_KEY, accessToken, cacheExpiration);

            return accessToken;
        }

        private async Task<HttpClient> GetAuthenticatedClient()
        {
            var token = await GetAccessToken();
            var client = _httpClientFactory.CreateClient("KeycloakClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task<KeycloakUserResponse?> GetUsuarioPorId(string userId)
        {
            var cacheKey = $"keycloak_user_{userId}";

            if (_cache.TryGetValue(cacheKey, out KeycloakUserResponse? cachedUser)) return cachedUser;

            try
            {
                var httpClient = await GetAuthenticatedClient();

                var userRes = await httpClient.GetAsync(
                    $"{BaseUrl}/admin/realms/{Realm}/users/{userId}"
                );

                if (!userRes.IsSuccessStatusCode) return null;

                var userJson = await userRes.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<KeycloakUserResponse>(userJson);

                if (user != null)
                {
                    var rolesTask = GetUserRealmRoles(userId);
                    var groupsTask = GetUserGroups(userId);

                    await Task.WhenAll(rolesTask, groupsTask);

                    user.realmRoles = await rolesTask;
                    user.groups = await groupsTask;

                    _cache.Set(cacheKey, user, TimeSpan.FromMinutes(5));
                }

                return user;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<string>> GetUserRealmRoles(string userId)
        {
            try
            {
                var httpClient = await GetAuthenticatedClient();

                var rolesRes = await httpClient.GetAsync(
                    $"{BaseUrl}/admin/realms/{Realm}/users/{userId}/role-mappings/realm"
                );

                if (!rolesRes.IsSuccessStatusCode) return new List<string>();

                var rolesJson = await rolesRes.Content.ReadAsStringAsync();
                var roles = JsonSerializer.Deserialize<List<JsonElement>>(rolesJson);

                return roles?
                    .Select(r => r.GetProperty("name").GetString() ?? "")
                    .Where(name => !string.IsNullOrEmpty(name))
                    .ToList() ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public async Task<List<string>> GetUserGroups(string userId)
        {
            try
            {
                var httpClient = await GetAuthenticatedClient();

                var groupsRes = await httpClient.GetAsync(
                    $"{BaseUrl}/admin/realms/{Realm}/users/{userId}/groups"
                );

                if (!groupsRes.IsSuccessStatusCode) return new List<string>();

                var groupsJson = await groupsRes.Content.ReadAsStringAsync();
                var groups = JsonSerializer.Deserialize<List<JsonElement>>(groupsJson);

                return groups?
                    .Select(g => g.GetProperty("name").GetString() ?? "")
                    .Where(name => !string.IsNullOrEmpty(name))
                    .ToList() ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public async Task<List<KeycloakUserResponse>> GetUsuariosMapeados()
        {
            var httpClient = await GetAuthenticatedClient();

            var usersRes = await httpClient.GetAsync(
                $"{BaseUrl}/admin/realms/{Realm}/users"
            );

            var usersJson = await usersRes.Content.ReadAsStringAsync();
            var rawUsers = JsonSerializer.Deserialize<List<KeycloakUserResponse>>(usersJson)
                ?? new List<KeycloakUserResponse>();

            var listaUsuarios = new List<KeycloakUserResponse>();

            var semaphore = new SemaphoreSlim(10);
            var tasks = rawUsers.Select(async kUser =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var rolesTask = GetUserRealmRoles(kUser.id);
                    var groupsTask = GetUserGroups(kUser.id);
                    await Task.WhenAll(rolesTask, groupsTask);

                    return new KeycloakUserResponse
                    {
                        id = kUser.id,
                        username = kUser.username,
                        firstName = kUser.firstName,
                        lastName = kUser.lastName,
                        email = kUser.email,
                        enabled = kUser.enabled,
                        emailVerified = kUser.emailVerified,
                        createdTimestamp = kUser.createdTimestamp,
                        attributes = kUser.attributes,
                        realmRoles = await rolesTask,
                        groups = await groupsTask
                    };
                }
                finally
                {
                    semaphore.Release();
                }
            });

            listaUsuarios = (await Task.WhenAll(tasks)).ToList();
            return listaUsuarios;
        }

        public async Task<List<KeycloakUserResponse>> GetUsuariosPorTipo(string tipoUsuario)
        {
            var todosUsuarios = await GetUsuariosMapeados();

            return todosUsuarios
                .Where(u => u.TipoUsuario?.Equals(tipoUsuario, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }

        public async Task<List<KeycloakUserResponse>> GetEstudiantesPorGrado(string grado, string? grupo = null)
        {
            var todosUsuarios = await GetUsuariosMapeados();

            var query = todosUsuarios
                .Where(u => u.TipoUsuario == "Estudiante" && u.Grado == grado);

            if (!string.IsNullOrEmpty(grupo))
            {
                query = query.Where(u => u.Grupo == grupo);
            }

            return query.ToList();
        }

        public async Task<List<KeycloakUserResponse>> GetProfesoresPorDepartamento(string departamento)
        {
            var todosUsuarios = await GetUsuariosMapeados();

            return todosUsuarios
                .Where(u => u.TipoUsuario == "Profesor" && u.Departamento == departamento)
                .ToList();
        }

        public async Task<List<KeycloakUserResponse>> BuscarUsuarios(
            string? email = null,
            string? firstName = null,
            string? lastName = null,
            string? username = null)
        {
            var httpClient = await GetAuthenticatedClient();

            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(email)) queryParams.Add($"email={email}");
            if (!string.IsNullOrEmpty(firstName)) queryParams.Add($"firstName={firstName}");
            if (!string.IsNullOrEmpty(lastName)) queryParams.Add($"lastName={lastName}");
            if (!string.IsNullOrEmpty(username)) queryParams.Add($"username={username}");

            var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

            var usersRes = await httpClient.GetAsync(
                $"{BaseUrl}/admin/realms/{Realm}/users{query}"
            );

            var usersJson = await usersRes.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<KeycloakUserResponse>>(usersJson)
                ?? new List<KeycloakUserResponse>();
        }

        public async Task<string> CrearUsuarioEnKeycloak(UsuarioDTO usuario, string password)
        {
            var httpClient = await GetAuthenticatedClient();

            var nuevoUsuario = new
            {
                username = usuario.Correo,
                email = usuario.Correo,
                firstName = usuario.Nombre,
                lastName = usuario.ApeLLido,
                enabled = true,
                emailVerified = false,
                credentials = new[]
                {
                    new { type = "password", value = password, temporary = false }
                },
            };

            var content = new StringContent(
                JsonSerializer.Serialize(nuevoUsuario),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync(
                $"{BaseUrl}/admin/realms/{Realm}/users",
                content
            );

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var location = response.Headers.Location
                    ?? throw new Exception("Keycloak no devolvió la ubicación del usuario");

                var userId = location.Segments.Last().TrimEnd('/');

                if (!Guid.TryParse(userId, out _))
                    throw new Exception($"ID inválido: {userId}");

                return userId;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("El usuario ya existe en Keycloak");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> EliminarUsuarioEnKeycloak(string userId)
        {
            try
            {
                var httpClient = await GetAuthenticatedClient();
                var response = await httpClient.DeleteAsync(
                    $"{BaseUrl}/admin/realms/{Realm}/users/{userId}"
                );
                if (response.IsSuccessStatusCode)
                {
                    _cache.Remove($"keycloak_user_{userId}");
                    _cache.Remove("keycloak_usuarios_mapeados");
                }
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CambiarPasswordUsuario(string userId, string nuevaPassword)
        {
            try
            {
                var httpClient = await GetAuthenticatedClient();

                var body = new
                {
                    type = "password",
                    value = nuevaPassword,
                    temporary = false
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PutAsync(
                    $"{BaseUrl}/admin/realms/{Realm}/users/{userId}/reset-password",
                    content
                );

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}