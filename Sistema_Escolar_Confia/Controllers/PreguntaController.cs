using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        private readonly ICearPreguntaService _service;

        public PreguntaController(ICearPreguntaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPregunta([FromBody] BancaPreguntaDTO dto)
        {
            if (dto == null)
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

            var resultado = await _service.CearPregunta(dto);

            if (resultado.Success)
            {
                return Ok(resultado);
            }

            return BadRequest(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPreguntas()
        {
            var resultado = await _service.ObtenerPreguntas();

            if (resultado.Success) return Ok(resultado);

            return BadRequest(resultado);
        }

        [HttpGet("banco/{bancaId}")]
        public async Task<IActionResult> ObtenerPreguntaPorId(int bancaId)
        {
            var resultado = await _service.ObtenerPreguntaPorId(bancaId);

            if (resultado.Success) return Ok(resultado);
            return NotFound(resultado);
        }

        [HttpPut("{bancaId}")]
        public async Task<IActionResult> ActualizarPregunta(int bancaId, [FromBody] BancaPreguntaDTO dto)
        {
            if (dto == null)
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

            var resultado = await _service.ActualizarPregunta(bancaId, dto);

            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }
    }
}
