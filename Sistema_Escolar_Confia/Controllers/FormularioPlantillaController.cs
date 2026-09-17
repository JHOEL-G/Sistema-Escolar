using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioPlantillaController : ControllerBase
    {
        private readonly IFormularioService _service;

        public FormularioPlantillaController(IFormularioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _service.ListarFormularios();
            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }

        [HttpGet("{plantillaId}")]
        public async Task<IActionResult> ObtenerPorId(int plantillaId)
        {
            var resultado = await _service.ObtenerFormularioPorId(plantillaId);

            if (resultado.Success) return Ok(resultado);
            return NotFound(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearFormularioDTO dto)
        {
            if (dto == null) return BadRequest("El cuerpo no puede estar vacío.");

            var resultado = await _service.CrearFormulario(dto);

            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }

        [HttpPut("{plantillaId}")]
        public async Task<IActionResult> Modificar(int plantillaId, [FromBody] CrearFormularioDTO dto)
        {
            if (dto == null) return BadRequest("El cuerpo no puede estar vacío.");

            var resultado = await _service.ModificarFormulario(plantillaId, dto);

            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }

        [HttpPatch("{plantillaId}/publicar")]
        public async Task<IActionResult> Publicar(int plantillaId, [FromQuery] bool publicar = true)
        {
            var resultado = await _service.PublicarFormulario(plantillaId, publicar);

            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }

        [AllowAnonymous]
        [HttpGet("public/{publicId}")]
        public async Task<IActionResult> ObtenerPorPublicId(Guid publicId)
        {
            var resultado = await _service.ObtenerFormularioPorPublicId(publicId);
            if (resultado.Success) return Ok(resultado);
            return NotFound(resultado);
        }

        [AllowAnonymous]
        [HttpPost("{plantillaId}/respuestas")]
        public async Task<IActionResult> GuardarRespuestas(int plantillaId, [FromBody] EnvioFormularioDTO dto)
        {
            dto.PlantillaId = plantillaId;
            var resultado = await _service.GuardarRespuestas(dto);
            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }

        [HttpGet("{plantillaId}/respuestas")]
        public async Task<IActionResult> ObtenerRespuestas(int plantillaId)
        {
            var resultado = await _service.ObtenerRespuestasFormulario(plantillaId);
            if (resultado.Success) return Ok(resultado);
            return NotFound(resultado);
        }

        [HttpGet("con-respuestas")]
        public async Task<IActionResult> ListarConRespuestas()
        {
            var resultado = await _service.ListarFormulariosConRespuestas();
            if (resultado.Success) return Ok(resultado);
            return BadRequest(resultado);
        }
    }
}
