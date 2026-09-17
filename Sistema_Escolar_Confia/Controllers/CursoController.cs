using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;
using Sistema_Escolar.Application.Services;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _service;

        public CursoController(ICursoService cursoService)
        {
            _service = cursoService;
        }

        [HttpPost]
        [RequestSizeLimit(1073741824)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> CrearCursos([FromForm] string cursoData, IFormFile? imagen, IFormFile? video, List<IFormFile>? archivoRecurso)
        {
            try
            { 
                var cursoRequest = JsonSerializer.Deserialize<CursoCreationRequest>(cursoData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (cursoRequest == null)
                {
                    return BadRequest(new { success = false, message = "Datos del curso inválidos" });
                }

                var cursoDTO = new CursoDTO
                {
                    NombreCurso = cursoRequest.NombreCurso,
                    HabilitarFechaCurso = cursoRequest.HabilitarFechaCurso,
                    DificultadId = cursoRequest.DificultadId,
                    LenguajeId = cursoRequest.LenguajeId,
                    DescripcionCurso = cursoRequest.DescripcionCurso,

                    CaracteristicasQueAprendere = cursoRequest.CaracteristicasQueAprendere != null && cursoRequest.CaracteristicasQueAprendere.Any()
                        ? string.Join(",", cursoRequest.CaracteristicasQueAprendere)
                        : null,

                    CaracteristicasHabilidades = cursoRequest.CaracteristicasHabilidades != null && cursoRequest.CaracteristicasHabilidades.Any()
                        ? string.Join(",", cursoRequest.CaracteristicasHabilidades)
                        : null,

                    CaracteristicasRequerimientos = cursoRequest.CaracteristicasRequerimientos != null && cursoRequest.CaracteristicasRequerimientos.Any()
                        ? string.Join(",", cursoRequest.CaracteristicasRequerimientos)
                        : null,

                    Avance = cursoRequest.Avance,
                    RetroalimentacionId = cursoRequest.RetroalimentacionId,
                    DuracionCurso = cursoRequest.DuracionCurso,
                    MensajeBienvenida = cursoRequest.MensajeBienvenida,
                    InstructorId = cursoRequest.InstructorId,

                    InstructorIds = cursoRequest.InstructorIds,


                    Reacreditacion = cursoRequest.Reacreditacion,
                    EstaPublicado = cursoRequest.EstaPublicado,
                    Modulos = cursoRequest.Modulos,
                    Recursos = cursoRequest.Recursos,
                    PorQueInscribirmeCurso = cursoRequest.PorQueInscribirmeCurso,
                    Calificacion = cursoRequest.Calificacion,
                    CursoReacreditacionId = cursoRequest.CursoReacreditacionId,
                    PeriodoVigencia = cursoRequest.PeriodoVigencia
                };

                using Stream? imagenStream = imagen?.OpenReadStream();
                using Stream? videoStream = video?.OpenReadStream();

                var resultado = await _service.CrearCurso(
                    cursoDTO,
                    imagenStream, imagen?.FileName,
                    videoStream, video?.FileName,
                    archivoRecurso
                );

                if (!resultado.Success)
                    Console.WriteLine($"!!! Error en Service: {resultado.Message}");

                return Ok(resultado);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { success = false, message = $"Error al deserializar datos: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCursos([FromQuery] bool soloActivos = true)
        {
            var resultado = await _service.ObtenerCursos(soloActivos);

            if (resultado != null && resultado.Data != null)
            {
                var cursos = resultado.Data as IEnumerable<CursoDTO>;
                if (cursos != null)
                {
                    foreach (var curso in cursos)
                    {
                        ConvertirStringsAArrays(curso);
                    }
                }
            }

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCursoPorId(int id)
        {
            var resultado = await _service.ObtenerCursoId(id);

            if (resultado != null && resultado.Data != null && resultado.Data is CursoDTO curso)
            {
                ConvertirStringsAArrays(curso);
            }

            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [RequestSizeLimit(1073741824)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> ActualizarCurso(int id, [FromForm] string cursoData, IFormFile? imagen, IFormFile? video, List<IFormFile>? archivoRecurso)
        {
            try
            {
                var cursoRequest = JsonSerializer.Deserialize<CursoCreationRequest>(cursoData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (cursoRequest == null) return BadRequest(new { success = false, message = "Datos inválidos" });

                var cursoDTO = new CursoDTO
                {
                    CursoId = id,
                    NombreCurso = cursoRequest.NombreCurso,
                    HabilitarFechaCurso = cursoRequest.HabilitarFechaCurso,
                    DificultadId = cursoRequest.DificultadId,
                    LenguajeId = cursoRequest.LenguajeId,
                    DescripcionCurso = cursoRequest.DescripcionCurso,

                    CaracteristicasQueAprendere = cursoRequest.CaracteristicasQueAprendere != null && cursoRequest.CaracteristicasQueAprendere.Any()
                        ? string.Join(",", cursoRequest.CaracteristicasQueAprendere)
                        : null,

                    CaracteristicasHabilidades = cursoRequest.CaracteristicasHabilidades != null && cursoRequest.CaracteristicasHabilidades.Any()
                        ? string.Join(",", cursoRequest.CaracteristicasHabilidades)
                        : null,

                    CaracteristicasRequerimientos = cursoRequest.CaracteristicasRequerimientos != null && cursoRequest.CaracteristicasRequerimientos.Any()
                        ? string.Join(",", cursoRequest.CaracteristicasRequerimientos)
                        : null,

                    Avance = cursoRequest.Avance,
                    RetroalimentacionId = cursoRequest.RetroalimentacionId,
                    DuracionCurso = cursoRequest.DuracionCurso,
                    MensajeBienvenida = cursoRequest.MensajeBienvenida,
                    InstructorId = cursoRequest.InstructorId,

                    InstructorIds = cursoRequest.InstructorIds,


                    Reacreditacion = cursoRequest.Reacreditacion,
                    EstaPublicado = cursoRequest.EstaPublicado,
                    Modulos = cursoRequest.Modulos,
                    Recursos = cursoRequest.Recursos,
                    PorQueInscribirmeCurso = cursoRequest.PorQueInscribirmeCurso,
                    Calificacion = cursoRequest.Calificacion,
                    CursoReacreditacionId = cursoRequest.CursoReacreditacionId,
                    PeriodoVigencia = cursoRequest.PeriodoVigencia
                };

                using Stream? imagenStream = imagen?.OpenReadStream();
                using Stream? videoStream = video?.OpenReadStream();

                var resultado = await _service.EditarCurso(
                    cursoDTO,
                    imagenStream, imagen?.FileName,
                    videoStream, video?.FileName,
                    archivoRecurso
                );

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        private void ConvertirStringsAArrays(CursoDTO curso)
        {
           
        }

        [HttpPost("{id}/duplicar")]
        public async Task<IActionResult> Duplicar(int id)
        {
            var resultado = await _service.DuplicarCurso(id);
            if (!resultado.Success) return BadRequest(new { success = false, message = resultado.Message });
            return Ok(new { success = true, message = resultado.Message, data = resultado.Data });
        }

        [HttpPost("{id}/recursos")]
        [RequestSizeLimit(1073741824)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> AgregarRecursos(int id,[FromForm] string recursosData, [FromForm] string? modulosData = null, List<IFormFile>? archivosRecursos = null)
        {
            try
            {
                var modulos = string.IsNullOrEmpty(modulosData)
                    ? new List<ModuloDTO>()
                    : JsonSerializer.Deserialize<List<ModuloDTO>>(modulosData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var recursos = JsonSerializer.Deserialize<List<RecursoDTO>>(recursosData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (recursos == null || !recursos.Any())
                    return BadRequest(new { success = false, message = "No se enviaron recursos" });

                var resultado = await _service.AgregarRecursosCurso(id, modulos!, recursos, archivosRecursos);

                if (!resultado.Success)
                    return BadRequest(new { success = false, message = resultado.Message });

                return Ok(resultado);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { success = false, message = $"Error al deserializar: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        
        [HttpPut("{id}/ponderacion")]
        public async Task<IActionResult> ActualizarPonderacion(int id, [FromBody] ActualizarPonderacionRequest request)
        {
            try
            {
                if (request == null || !request.Ponderaciones.Any())
                    return BadRequest(new { success = false, message = "No se enviaron ponderaciones" });

                var resultado = await _service.ActualizarPonderacion(
                    id,
                    request.Ponderaciones,
                    request.Calificacion,
                    request.RequisitoAvance);

                if (!resultado.Success)
                    return BadRequest(new { success = false, message = resultado.Message });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPatch("{id:int}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoCursoRequest request)
        {
            var resultado = await _service.CambiarEstadoCurso(id, request.AdminId, request.Accion, request.Motivo);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarCurso(int id, [FromBody] EliminarCursoRequest request)
        {
            var resultado = await _service.EliminarCurso(id, request.AdminId, request.Motivo);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
