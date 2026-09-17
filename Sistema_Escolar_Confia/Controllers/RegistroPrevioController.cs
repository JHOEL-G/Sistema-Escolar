using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;
using System.Text.Json;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroPrevioController : ControllerBase
    {
        private readonly IRegistroPrevioService _service;

        public RegistroPrevioController(IRegistroPrevioService registroPrevioService)
        {
            _service = registroPrevioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRegistro([FromForm] string registroData, IFormFile? imagen)
        {
            try
            {
                var registroDto = JsonSerializer.Deserialize<RegistroPrevioDTO>(registroData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (registroDto == null) return BadRequest( new { message = "Datos de registro inválidos." });

                Stream? imagenStream = imagen?.OpenReadStream();

                var resultado = await _service.CrearRegistroPrevio(registroDto, imagenStream, imagen?.FileName);

                return Ok(resultado);
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

        [HttpGet]
        public async Task<IActionResult> ObtenerRegistrosPrevios()
        {
            var resultado = await _service.ObtenerRegistrosPrevios();

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRegistroPorId(int id)
        {
            var resultado = await _service.ObtenerRegistroPrevioPorId(id);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRegistro([FromBody] RegistroPrevioDTO registroPrevioDTO)
        {
            var resultado = await _service.EditarRegistroPrevio(registroPrevioDTO);

            return Ok(resultado);
        }
    }
}
