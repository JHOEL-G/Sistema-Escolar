using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaAprendizajeController : ControllerBase
    {
        private readonly IRutaAprendizajeService _service;

        public RutaAprendizajeController(IRutaAprendizajeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarRutaAprendizaje()
        {
            var resultado = await _service.ObtenerRutaAaprendizaje();

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRutaAprendizaje([FromForm] string rutaAprendizajeData, IFormFile? imagen)
        {
            try
            {
                var resultado = JsonSerializer.Deserialize<RutaAprendizajeDTO>(rutaAprendizajeData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (resultado == null) return BadRequest(new { message = "Datos de registro inválidos." });

                Stream? imagenStream = imagen?.OpenReadStream();

                var resul = await _service.CrearRutaAprendizaje(resultado, imagenStream, imagen?.FileName);

                return Ok(resul);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { success = false, message = $"Error al deserializar datos: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpGet("{rutaId}/participantes")]
        public async Task<IActionResult> GetParticipantes(int rutaId)
        {
            var resultado = await _service.ObtenerParticipantesRuta(rutaId);

            if (!resultado.Success)
                return BadRequest(new { success = false, message = resultado.Message });

            return Ok(new { success = true, data = resultado.Data });
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetRutasPorUsuario(int usuarioId)
        {
            var resultado = await _service.ObtenerRutasPorUsuario(usuarioId);

            if (!resultado.Success)
                return BadRequest(new { success = false, message = resultado.Message });

            return Ok(new { success = true, data = resultado.Data });
        }

        [HttpGet("{rutaId}")]
        public async Task<IActionResult> ObtenerRutaAprendizajeId(int rutaId)
        {
            var resultado = await _service.ObtenerRutaAprendizajePorId(rutaId);

            if (!resultado.Success)  
                return BadRequest(new { success = false, message = resultado.Message });

            return Ok(new { success = true, data = resultado.Data });
        }

        [HttpPut("{rutaId}")]
        public async Task<IActionResult> ActualizarRutaAprendizaje([FromRoute] int rutaId, [FromForm] string rutaAprendizajeData, IFormFile? imagen)
        {
            try
            {
                var dto = JsonSerializer.Deserialize<RutaAprendizajeDTO>(rutaAprendizajeData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dto == null) return BadRequest(new { message = "Datos de registro inválidos." });

                Stream? imagenStream = imagen?.OpenReadStream();

                var resul = await _service.ActualizarRutaAprendizaje(rutaId, dto, imagenStream, imagen?.FileName); 

                return Ok(resul);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { success = false, message = $"Error al deserializar datos: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
