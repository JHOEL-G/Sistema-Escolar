using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionCursoController : ControllerBase
    {
        private readonly IGestionCursoService _service;

        public GestionCursoController(IGestionCursoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearGestionCurso([FromBody] CrearGestionCursoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _service.CrearGestionCurso(dto);

            if (!resultado.Success)
            {
                return BadRequest(new { mensaje = resultado.Message });
            }


            return Ok(new
            {
                mensaje = resultado.Message,
                gestionCursoId = resultado.Data 
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerIdGestionCurso(int id)
        {
            var resultado = await _service.GetIdGestiomCurso(id);

            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarGestionCurso([FromBody] CrearGestionCursoDTO dto)
        {
            dto.GestionCursoId = dto.GestionCursoId ?? 0;

            var resultado = await _service.ActualizarGestionCurso(dto);

            if (!resultado.Success) return BadRequest(new { mensaje = resultado.Message });

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ListarGestionCurso()
        {
            var resultado = await _service.ListarGestionCursos();

            return Ok(resultado);
        }
    }
}
