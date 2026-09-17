using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _service;

        public RolController(IRolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var reultado = await _service.ObtenerRoles();

            if (!reultado.Success) return NotFound(new { mensaje = reultado.Message });

            return Ok(reultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var rol = await _service.ObtenerRolId(id);
            if (rol == null) return NotFound(new { mensaje = "Rol no encontrado" });
            return Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] CatRolDTO catRolDTO)
        {
            var resultado = await _service.CrearRol(catRolDTO);

            if (!resultado.Success) return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] CatRolDTO catRolDTO)
        {
            if (id != catRolDTO.RolId) return BadRequest("El ID del rol no coincide");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _service.EditarRol(catRolDTO);

            if (!resultado.Success) return BadRequest(new { mensaje = resultado.Message });

            return Ok(new { mensaje = resultado.Message });
        }
    }
}
