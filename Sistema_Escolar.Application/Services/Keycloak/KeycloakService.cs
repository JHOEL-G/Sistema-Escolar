using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.APIs;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IKeycloakService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services.Keycloak
{
    public class KeycloakService : IKeycloakService
    {
        private readonly IUsuarioRepository _repo;
        private readonly KeycloakDataExtractor _keycloakDataExtractor;
        private readonly IFileStorageService _service;


        public KeycloakService(IUsuarioRepository repo, KeycloakDataExtractor keycloakDataExtractor, IFileStorageService service)
        {
            _service = service;
            _repo = repo;
            _keycloakDataExtractor = keycloakDataExtractor;
        }

        public async Task<OperationResult> CrearUsuarioKeycloak(UsuarioDTO usuario, string password, Stream? imagenStream = null, string? nombreImagen = null)
        {
            string? keycloakId = null;

            try
            {
                if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
                    usuario.ImagenPortada = await _service.UploadFile(imagenStream, nombreImagen, "usuarios-avatar");

                keycloakId = await _keycloakDataExtractor.CrearUsuarioEnKeycloak(usuario, password);
                usuario.KeycloakId = Guid.Parse(keycloakId);

                var resultado = await _repo.InsertarUsuarioAsync(usuario, "KEYCLOAK_MANAGED_USER");

                if (!resultado.Success)
                {
                    await _keycloakDataExtractor.EliminarUsuarioEnKeycloak(keycloakId);
                    return OperationResult.Fail($"Error al guardar en base de datos. Se revirtió la creación en Keycloak. Detalle: {resultado.Message}");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(keycloakId))
                {
                    var eliminado = await _keycloakDataExtractor.EliminarUsuarioEnKeycloak(keycloakId);
                    if (!eliminado)
                        return OperationResult.Fail($"Error crítico: usuario {keycloakId} quedó en Keycloak sin registrarse en DB. Elimínalo manualmente. Error: {ex.Message}");
                }

                return OperationResult.Fail($"Error en flujo de creación: {ex.Message}");
            }
        }

        public async Task<List<OperationResult>> SincronizarUsuariosDesdeKeycloak()
        {
            var resultados = new List<OperationResult>();
            var usuariosKeycloak = await _keycloakDataExtractor.GetUsuariosMapeados();
            var usuariosExistentes = await _repo.GetUsuarios(null);

            foreach (var kUser in usuariosKeycloak)
            {
                bool existe = usuariosExistentes.Any(u =>
                    u.Correo.Equals(kUser.email, StringComparison.OrdinalIgnoreCase) ||
                    u.KeycloakId.ToString().Equals(kUser.id, StringComparison.OrdinalIgnoreCase)
                );

                if (!existe)
                {
                    var usuarioDto = new UsuarioDTO
                    {
                        KeycloakId = Guid.Parse(kUser.id),
                        Nombre = kUser.firstName,
                        ApeLLido = kUser.lastName,
                        Correo = kUser.email,
                        Activo = kUser.enabled,
                        Curp = kUser.CURP,
                        IdEmpleado = kUser.IdEmpleado,
                        RolId = MapearRolDesdeKeycloak(kUser),
                    };

                    var resultado = await _repo.InsertarUsuarioAsync(
                        usuarioDto,
                        "KEYCLOAK_MANAGED_USER"
                    );

                    resultados.Add(resultado);
                }
            }

            return resultados;
        }

        public async Task<OperationResult> CambiarPasswordKeycloak(Guid keycloakId, string nuevaPassword)
        {
            try
            {
                var resultado = await _keycloakDataExtractor.CambiarPasswordUsuario(
                    keycloakId.ToString(),
                    nuevaPassword
                );

                if (!resultado)
                    return OperationResult.Fail("No se pudo actualizar la contraseña en Keycloak");

                return OperationResult.Ok("Contraseña actualizada correctamente");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Error al cambiar contraseña: {ex.Message}");
            }
        }

        public async Task<OperationResult> EliminarUsuarioKeycloak(Guid keycloakId)
        {
            try
            {
                var eliminado = await _keycloakDataExtractor.EliminarUsuarioEnKeycloak(keycloakId.ToString());

                if (!eliminado)
                    return OperationResult.Fail("No se pudo eliminar el usuario de Keycloak");

                return OperationResult.Ok("Usuario eliminado de Keycloak correctamente");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Error al eliminar de Keycloak: {ex.Message}");
            }
        }

        private int? MapearRolDesdeKeycloak(KeycloakUserResponse kUser)
        {
            if (kUser.realmRoles.Contains("admin") || kUser.groups.Contains("Administradores"))
                return 1;

            if (kUser.TipoUsuario == "Profesor" || kUser.groups.Contains("Profesores"))
                return 2;

            if (kUser.TipoUsuario == "Estudiante" || kUser.groups.Contains("Estudiantes"))
                return 3;

            return null;
        }
    }
}
