using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly IUsuarioCursoService _service;

        public InscripcionController(IUsuarioCursoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearInscripcion([FromBody] UsuarioCursoDTO dto)
        {
            var resultado = await _service.CrearInscripcionCurso(dto);

            if (!resultado.Success) return BadRequest(resultado.Message);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UsuarioCursoId(string id)
        {
            var resultado = await _service.ObtenerCursoUsuarioId(id);

            if (!resultado.Success) return Ok(new { success = true, data = new List<object>(), message = resultado.Message });

            return Ok(resultado);
        }

        [HttpGet("participantes/{cursoId}")]
        public async Task<IActionResult> ObtenerParticipantes(int cursoId)
        {
            var resultado = await _service.ObtenerParticipantes(cursoId);

            if (!resultado.Success) return NotFound(resultado.Message);

            return Ok(resultado);
        }

        [HttpPost("progreso")]
        public async Task<IActionResult> RegistrarProgreso([FromBody] ProgresoDTO dto)
        {
            var resultado = await _service.RegistrarProgresoRecurso(dto);
            if (!resultado.Success) return BadRequest(resultado.Message);
            return Ok(resultado);
        }

        [HttpPost("calificar")]
        public async Task<IActionResult> CalificarRecurso([FromBody] CalificacionRecursoDTO dto)
        {
            var resultado = await _service.CalificarRecurso(dto);
            if (!resultado.Success) return BadRequest(resultado.Message);
            return Ok(resultado);
        }

        [HttpGet("calificaciones/{cursoId}")]
        public async Task<IActionResult> ObtenerCalificaciones(int cursoId)
        {
            var resultado = await _service.ObtenerCalificaciones(cursoId);
            if (!resultado.Success) return NotFound(resultado.Message);
            return Ok(resultado);
        }

        [HttpGet("progreso/{usuarioId}/{cursoId}")]
        public async Task<IActionResult> ObtenerProgreso(int usuarioId, int cursoId)
        {
            var resultado = await _service.ObtenerProgreso(usuarioId, cursoId);
            return Ok(new { success = true, data = resultado });
        }

        [HttpGet("participantes-recursos/{cursoId}")]
        public async Task<IActionResult> ObtenerParticipantesConRecursos(int cursoId)
        {
            var resultado = await _service.ObtenerParticipantesConRecursos(cursoId);
            if (!resultado.Success) return NotFound(resultado.Message);
            return Ok(resultado);
        }

        [HttpGet("respuestas-alumno/{evaluacionId}/{usuarioId}")]
        public async Task<IActionResult> ObtenerRespuestasAlumno(int evaluacionId, int usuarioId)
        {
            var resultado = await _service.ObtenerRespuestasAlumno(evaluacionId, usuarioId);

            if (!resultado.Success) return NotFound(resultado.Message);

            return Ok(resultado);
        }

        [HttpPost("guardar-respuestas")]
        public async Task<IActionResult> GuardarRespuestas([FromBody] GuardarRespuestasDTO dto)
        {
            var resultado = await _service.GuardarRespuestasEvaluacion(dto);
            if (!resultado.Success) return BadRequest(resultado.Message);
            return Ok(resultado);
        }

        [HttpGet("verificar/{usuarioId}/{cursoId}")]
        public async Task<IActionResult> VerificarInscripcion(int usuarioId, int cursoId)
        {
            var resultado = await _service.VerificarInscripcion(usuarioId, cursoId);
            return Ok(new { inscrito = resultado });
        }
    }
}
