using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly IPropiedadService _service;

        public PropiedadController(IPropiedadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropiedades()
        {
            var resultado = await _service.ObtenerPropiedades();

            if (!resultado.Success) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPropiedadPorId(int id)
        {
            var propiedad = await _service.ObtenerPropiedadId(id);
            if (!propiedad.Success) return NotFound(propiedad);
            return Ok(propiedad);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPropiedad([FromBody] PropiedadDTO propiedadDTO)
        {
            var resultado = await _service.CrearPropiedad(propiedadDTO);

            if(!resultado.Success) return BadRequest(new { mensaje = resultado.Message});

            return Ok(new { mensaje = resultado.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPropiedad(int id, [FromBody] PropiedadDTO propiedadDTO)
        {
            if (id != propiedadDTO.PropiedadId) return BadRequest(new { mensaje = "El ID de la URL no coincide con el cuerpo de la solicitud" });

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _service.EditarPropiedad(propiedadDTO);

            if (!resultado.Success) return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message});
        }
    }
}
