using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IKeycloakService;
using Sistema_Escolar.Application.Interfaces.IServices;
using Sistema_Escolar.Application.Services;
using Sistema_Escolar.Application.Services.Keycloak;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IKeycloakService _serviceK;

        public UsuarioController(IUsuarioService service, IKeycloakService serrviceK)
        {
            _service = service;
            _serviceK = serrviceK;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios([FromQuery] bool? activo)
        {
            var resultado = await _service.ObtenerUsuario(activo);

            if (!resultado.Success)
            {
                return NotFound(new { mensaje = resultado.Message });
            }

            return Ok(resultado.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuario = await _service.ObtenerUsuarioId(id);
            if (usuario == null) return NotFound("Usuario no encontrado");
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var resultado = await _service.CrearUsuario(usuarioDTO);

            if (!resultado.Success)
                return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromForm] string UsuarioData, IFormFile? imagen)
        {
            var dto = JsonSerializer.Deserialize<UsuarioDTO>(UsuarioData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dto == null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            if (id != dto.UsuarioId)
                return BadRequest(new { mensaje = "El ID no coincide" });

            if (string.IsNullOrWhiteSpace(dto.Correo))
                return BadRequest(new { mensaje = "El correo es obligatorio" });

            try
            {
                Stream? imagenStream = imagen?.OpenReadStream();
                string? nombreImagen = imagen?.FileName;

                var resultado = await _service.EditarUsuario(dto, imagenStream, nombreImagen);

                if (!resultado.Success)
                    return BadRequest(new { mensaje = resultado.Message });

                return Ok(new { mensaje = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar usuario", error = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> ObtenerUsuarioActual()
        {
            var keycloakIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");

            if (keycloakIdClaim == null) return Unauthorized();

            var guid = Guid.Parse(keycloakIdClaim.Value);

            var usuario = await _service.ObtenerPerfilLogin(guid);

            if (usuario == null) return NotFound("Usuario no sincronizado en DB local");

            return Ok(usuario);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var usuario = await _service.ObtenerUsuarioId(id);
            if (usuario == null) return NotFound(new { mensaje = "Usuario no encontrado" });

            usuario.Activo = !(usuario.Activo ?? false);
            usuario.ImagenPortada = null;
            usuario.Contraseña = null;

            var resultado = await _service.EditarUsuario(usuario);

            if (!resultado.Success)
                return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = $"Usuario {(usuario.Activo.Value ? "activado" : "desactivado")} correctamente" });
        }

        [HttpPatch("{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordDTO model)
        {
            var usuario = await _service.ObtenerUsuarioId(id);
            if (usuario == null) return NotFound(new { mensaje = "Usuario no encontrado" });

            if (usuario.KeycloakId == null)
                return BadRequest(new { mensaje = "El usuario no está vinculado a Keycloak" });

            var resultado = await _serviceK.CambiarPasswordKeycloak(
                usuario.KeycloakId.Value,
                model.NuevaPassword
            );

            if (!resultado.Success)
                return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = "Contraseña actualizada exitosamente" });
        }

        [HttpGet("puestos")]
        public async Task<IActionResult> ObtenerPuestos()
        {
            var resultado = await _service.ObtenerPuesto();
            if (!resultado.Success)
                return NotFound(new { mensaje = resultado.Message });
            return Ok(resultado.Data);
        }

        [HttpGet("jefes")]
        public async Task<IActionResult> ObtenerJefes()
        {
            var resultado = await _service.ObtenerJefe();

            if (!resultado.Success)
                return NotFound(new { mensaje = resultado.Message });

            return Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _service.ObtenerUsuarioId(id);
            if (usuario == null) return NotFound(new { mensaje = "Usuario no encontrado" });

            if (usuario.KeycloakId != null)
            {
                var eliminadoKeycloak = await _serviceK.EliminarUsuarioKeycloak(usuario.KeycloakId.Value);
                if (!eliminadoKeycloak.Success)
                    return BadRequest(new { mensaje = $"Error al eliminar de Keycloak: {eliminadoKeycloak.Message}" });
            }

            var resultado = await _service.EliminarUsuario(id);

            if (!resultado.Success)
                return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message });
        }
    }
}
