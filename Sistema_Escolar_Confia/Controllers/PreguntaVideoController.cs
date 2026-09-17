using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaVideoController : ControllerBase
    {
        private readonly IPreguntaVideoService _service;

        public PreguntaVideoController(IPreguntaVideoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVideoId(int id)
        {
            var resultado = await _service.ObtenerPreguntasPorId(id);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPreguntaVideo([FromForm] string videoData, IFormFile? videoFile)
        {
            try
            {
                var videodto = JsonSerializer.Deserialize<PreguntaVideoDTO>(videoData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (videodto == null) return BadRequest(new { message = "Datos de registro inválidos." });

                if (videoFile != null)
                {
                    using (Stream videoStream = videoFile.OpenReadStream())
                    {
                        var resultado = await _service.CrearPreguntaVideo(videodto, videoStream, videoFile.FileName);
                        return Ok(resultado);
                    }
                }

                var resultadoSinVideo = await _service.CrearPreguntaVideo(videodto);

                return Ok(resultadoSinVideo);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { success = false, message = $"Error al deserializar datos: {ex.Message}" });
            }
            catch (Exception ex)
            {
                {
                    return StatusCode(500, new { success = false, message = $"Error interno del servidor: {ex.Message}" });
                }
            }
        }
    }
}
