using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.InterfaceGeneralService;
using Sistema_Escolar_Confia.Models;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoModuloController : ControllerBase
    {
        private readonly IRecursoModuloService _service;

        public RecursoModuloController(IRecursoModuloService service)
        {
            _service = service;
        }

        [HttpPost("evaluacionPrecencial")]
        public async Task<IActionResult> CrearEvaluacionPresencial(RecursoEvaluacionPresencialDTO dto)
            => Ok(await _service.EvaluacionPrecencial.CrearEvaluacionPresencial(dto));

        [HttpPost("sesion")]
        public async Task<IActionResult> CrearSesion(RecursoSesionPresencialDTO dto)
            => Ok(await _service.Sesion.CrearSesionPresencial(dto));

        [HttpPost("encuesta")]
        public async Task<IActionResult> CrearEncuesta(RecursoEncuestaDTO dto)
            => Ok(await _service.Encuesta.CrearEncuesta(dto));

        [HttpPost("zoom")]
        public async Task<IActionResult> CrearZoom(RecursoZoomDTO dto)
            => Ok(await _service.Zoom.CrearZoom(dto));

        [HttpPost("embebido")]
        public async Task<IActionResult> CrearEmbebido(RecursoEmbebidoDTO dto)
            => Ok(await _service.Embebido.CrearEmbebido(dto));

        [HttpPost("video")]
        public async Task<IActionResult> CrearVideo(
       [FromForm] RecursoVideoDTO dto, IFormFile? archivo = null)
        {
            using var stream = archivo?.OpenReadStream();
            return Ok(await _service.Video.CrearVideo(dto, stream, archivo?.FileName));
        }

        [HttpPost("lectura")]
        public async Task<IActionResult> CrearLectura(
            [FromForm] RecursoLecturaDTO dto, IFormFile? archivo = null)
        {
            using var stream = archivo?.OpenReadStream();
            return Ok(await _service.Lectura.CrearLectura(dto, stream, archivo?.FileName));
        }



        [HttpPost("foro")]
        public async Task<IActionResult> CrearForo([FromForm] RecursoForoDTO dto, List<IFormFile>? archivos = null)
        {
            var archivosStream = archivos?
                .Select(a => ((Stream)a.OpenReadStream(), a.FileName))
                .ToList();
            return Ok(await _service.Foro.CrearForo(dto, archivosStream));
        }

        [HttpPost("foro/publicar")]
        public async Task<IActionResult> PublicarForo([FromBody] PublicarForoDTO dto)
            => Ok(await _service.Foro.PublicarForo(dto));

        [HttpGet("foro/{foroId}/publicaciones")]
        public async Task<IActionResult> ObtenerPublicaciones(int foroId)
            => Ok(await _service.Foro.ListarForoPublicado(foroId));



        [HttpPost("tarea")]
        public async Task<IActionResult> CrearTarea([FromForm] RecursoTareaDTO dto, List<IFormFile>? archivos = null)
        {
            var archivosStream = archivos?
                .Select(a => ((Stream)a.OpenReadStream(), a.FileName))
                .ToList();
            return Ok(await _service.Tarea.CrearTarea(dto, archivosStream));
        }

        [HttpPost("tarea/entregar")]
        public async Task<IActionResult> EntregarTarea(
            [FromForm] EntregarTareaDTO dto, IFormFile? archivo = null)
        {
            using var stream = archivo?.OpenReadStream();
            return Ok(await _service.Tarea.EntregarTarea(dto, stream, archivo?.FileName));
        }

        [HttpGet("tarea/{tareaId}/entrega/{usuarioId}")]
        public async Task<IActionResult> ObtenerEntregaTarea(int tareaId, int usuarioId)
            => Ok(await _service.Tarea.ListarTareaEntregados(tareaId, usuarioId));



        [HttpPost("scorm")]
        public async Task<IActionResult> CrearScorm(
            [FromForm] RecursoScormDTO dto, IFormFile? archivo = null)
        {
            using var stream = archivo?.OpenReadStream();
            return Ok(await _service.Scorm.CrearScorm(dto, stream, archivo?.FileName));
        }

        [HttpPost("evaluacion")]
        public async Task<IActionResult> CrearEvaluacion(
            [FromForm] RecursoEvaluacionDTO dto, List<IFormFile>? archivos = null)
        {
            var archivosStream = archivos?
                .Select(a => (stream: (Stream)a.OpenReadStream(), nombre: a.FileName))
                .ToList();
            return Ok(await _service.Evaluacion.CrearEvaluacion(dto, archivosStream));
        }
    }
}
