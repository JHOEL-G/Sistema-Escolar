using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionDocumentoController : ControllerBase
    {
        private readonly IGestionDocumentoService _service;

        public GestionDocumentoController(IGestionDocumentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarArbolCarpetas()
        {
            var resultado = await _service.ListarArbolCarpetas();

            if (resultado == null) return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCarpeta(int id, [FromBody] CarpetaActualizarDTO dto)
        {
            dto.ExploradorId = id;

            var resultado = await _service.ActualizarCarpeta(dto);

            if (resultado == null) return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCarpeta([FromBody]CarpetaInsertarDTO dto)
        {
            var resultado = await _service.CrearCarpeta(dto);

            if (resultado == null) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCarpeta(int id)
        {
            var resultado = await _service.EliminarCarpeta(id);

            if (resultado == null) return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
