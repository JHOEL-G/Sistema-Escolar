using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OuController : ControllerBase
    {
        private readonly IOuService _service;

        public OuController(IOuService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<IActionResult> GetOus()
        {
            var resultado = await _service.ObtenerOus();

            if (!resultado.Success) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var ou = await _service.ObtenerOuId(id);
            if (!ou.Success) return NotFound(ou);
            return Ok(ou);
        }

        [HttpPost]
        public async Task<IActionResult> CrearOu([FromBody] OuDTO ouDTO)
        {
            var resultado = await _service.CrearOu(ouDTO);

            if (!resultado.Success) return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOu(int id, [FromBody] OuDTO ouDTO)
        {
            if (id != ouDTO.OrganizacionalesId)
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el cuerpo de la solicitud" });

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _service.EditarOu(ouDTO);

            if (!resultado.Success) return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message });
        }
    }
}
