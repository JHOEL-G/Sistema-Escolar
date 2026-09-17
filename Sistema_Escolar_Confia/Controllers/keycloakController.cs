using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sistema_Escolar.Application.APIs;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IKeycloakService;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class keycloakController : ControllerBase
    {
        private readonly IKeycloakService _service;
        private readonly KeycloakDataExtractor _keycloakDataExtractor;
        private readonly IMemoryCache _cache;

        public keycloakController(IKeycloakService service , KeycloakDataExtractor keycloakDataExtractor, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
            _keycloakDataExtractor = keycloakDataExtractor;
        }

        [HttpPost("sincronizar")]
        public async Task<IActionResult> SincronizarUsuarios()
        {
            var resultados = await _service.SincronizarUsuariosDesdeKeycloak();

            _cache.Remove("keycloak_usuarios_mapeados");

            return Ok(new
            {
                Mensaje = "Sincronización finalizada",
                Detalles = resultados,
                TotalProcesados = resultados.Count,
                Exitosos = resultados.Count(r => r.Success)
            });
        }

        [HttpGet("keycloak/estudiantes/{grado}")]
        public async Task<IActionResult> GetEstudiantesPorGrado(string grado, [FromQuery] string? grupo = null)
        {
            var cacheKey = $"estudiantes_{grado}_{grupo ?? "todos"}";

            if (_cache.TryGetValue(cacheKey, out List<KeycloakUserResponse>? cachedEstudiantes))
            {
                return Ok(cachedEstudiantes);
            }

            var estudiantes = await _keycloakDataExtractor.GetEstudiantesPorGrado(grado, grupo);

            _cache.Set(cacheKey, estudiantes, TimeSpan.FromMinutes(5));

            return Ok(estudiantes);
        }

        [HttpGet("keycloak/profesores/{departamento}")]
        public async Task<IActionResult> GetProfesoresPorDepartamento(string departamento)
        {
            var cacheKey = $"profesores_{departamento}";

            if (_cache.TryGetValue(cacheKey, out List<KeycloakUserResponse>? cachedProfesores))
            {
                return Ok(cachedProfesores);
            }

            var profesores = await _keycloakDataExtractor.GetProfesoresPorDepartamento(departamento);

            _cache.Set(cacheKey, profesores, TimeSpan.FromMinutes(5));

            return Ok(profesores);
        }

        [HttpGet("keycloak/buscar")]
        public async Task<IActionResult> BuscarUsuarios(
            [FromQuery] string? email,
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] string? username)
        {
            var cacheKey = $"busqueda_{email}_{firstName}_{lastName}_{username}";

            if (_cache.TryGetValue(cacheKey, out List<KeycloakUserResponse>? cachedResults))
            {
                return Ok(cachedResults);
            }

            var usuarios = await _keycloakDataExtractor.BuscarUsuarios(
                email,
                firstName,
                lastName,
                username
            );

            _cache.Set(cacheKey, usuarios, TimeSpan.FromMinutes(1));

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string UsuarioData, IFormFile? imagen)
        {
            var dto = JsonSerializer.Deserialize<UsuarioDTO>(UsuarioData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (string.IsNullOrWhiteSpace(dto.Contraseña))
                return BadRequest("La contraseña es obligatoria");

            if (dto.Contraseña.Length < 8)
                return BadRequest("La contraseña debe tener al menos 8 caracteres");

            if (dto.NivelPermisoId == null || dto.NivelPermisoId <= 0)
                return BadRequest("El Nivel de Permiso es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                return BadRequest("El correo es obligatorio");

            try
            {
                Stream? imagenStream = imagen?.OpenReadStream();
                string? nombreImagen = imagen?.FileName;

                var resultado = await _service.CrearUsuarioKeycloak(dto, dto.Contraseña, imagenStream, nombreImagen);

                if (resultado.Success)
                {
                    _cache.Remove("keycloak_usuarios_mapeados");
                    if (!string.IsNullOrEmpty(dto.Correo))
                    {
                        _cache.Remove($"busqueda_{dto.Correo}___");
                    }

                    return Ok(new
                    {
                        Mensaje = "Usuario creado y vinculado exitosamente",
                        Data = resultado
                    });
                }

                return BadRequest(new
                {
                    Mensaje = resultado.Message,
                    Error = resultado.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Mensaje = "Error al crear usuario",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("cache/clear")]
        public IActionResult ClearCache([FromQuery] string? pattern = null)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return Ok(new { Mensaje = "Para limpiar todo el caché, reinicia la aplicación" });
            }

            _cache.Remove(pattern);

            return Ok(new { Mensaje = $"Caché '{pattern}' eliminado" });
        }

        [HttpGet("cache/stats")]
        public IActionResult GetCacheStats()
        {
            return Ok(new
            {
                Mensaje = "Estadísticas de caché no disponibles con IMemoryCache",
                Sugerencia = "Considera usar Redis para caché distribuido con estadísticas"
            });
        }
    }
}
