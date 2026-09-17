using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionService _service;

        public NotificacionesController(INotificacionService service)
        {
            _service = service;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMias()
        {
            var keycloakIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (keycloakIdClaim == null)
                return Unauthorized(new { success = false, message = "Token inválido." });

            var usuarioId = await _service.ObtenerUsuarioIdPorKeycloakId(Guid.Parse(keycloakIdClaim.Value));
            if (usuarioId == null)
                return Unauthorized(new { success = false, message = "Usuario no encontrado." });

            var resultado = await _service.ListarPorUsuario(usuarioId.Value);
            return Ok(new { success = true, data = resultado.Data });
        }

        [HttpPatch("{id}/leer")]
        public async Task<IActionResult> MarcarLeida(int id)
        {
            var resultado = await _service.MarcarLeida(id);
            if (!resultado.Success) return BadRequest(new { success = false, message = resultado.Message });
            return Ok(new { success = true, message = resultado.Message });
        }

        [HttpDelete("vaciar")]
        public async Task<IActionResult> Vaciar()
        {
            var keycloakIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (keycloakIdClaim == null)
                return Unauthorized();

            var usuarioId = await _service.ObtenerUsuarioIdPorKeycloakId(Guid.Parse(keycloakIdClaim.Value));
            if (usuarioId == null) return Unauthorized();

            var resultado = await _service.EliminarTodas(usuarioId.Value);
            return Ok(new { success = true, message = resultado.Message });
        }
    }
}
