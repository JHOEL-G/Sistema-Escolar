using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;
using System.Text.Json;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _service;

        public TemaController(ITemaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTema([FromForm] string temaData, IFormFile? imagen)
        {
            try
            {
                var temaDto = JsonSerializer.Deserialize<TtemaDTO>(temaData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (temaDto == null) return BadRequest(new { message = "Datos de registro invalido" });

                Stream? imagenStream = imagen?.OpenReadStream();

                var resultado = await _service.CrearTema(temaDto, imagenStream, imagen?.FileName);

                return Ok(resultado);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { message = "Error al deserializar los datos del tema.", details = ex.Message });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTemas()
        {
            var resultado = await _service.ObtenerTemas();

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTemaPorId(int id)
        {
            var resultado = await _service.ObtenerTemaId(id);

            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarTema([FromForm] string temaData, IFormFile? imagen)
        {
            try
            {
                var temaDto = JsonSerializer.Deserialize<TtemaDTO>(temaData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (temaDto == null) return BadRequest(new { message = "Datos de registro inválidos" });

                if (imagen != null)
                {
                    using var imagenStream = imagen.OpenReadStream();
                    var resultado = await _service.ActualizarTema(temaDto, imagenStream, imagen.FileName);
                    return Ok(resultado);
                }
                else
                {
                    var resultado = await _service.ActualizarTema(temaDto);
                    return Ok(resultado);
                }
            }
            catch (JsonException ex)
            {
                return BadRequest(new { message = "Formato de datos JSON incorrecto.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al actualizar.", details = ex.Message });
            }
        }
    }
}
