using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.InterfaceGeneralService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repo;
        private readonly IFileStorageService _service;
        private readonly IRecursoModuloService _reservice;
        private readonly INotificacionService _serviceNoti;

        public CursoService(ICursoRepository repo, IFileStorageService service, IRecursoModuloService reservice, INotificacionService serviceNoti)
        {
            _repo = repo;
            _service = service;
            _reservice = reservice;
            _serviceNoti = serviceNoti;
        }

        public async Task<OperationResult> CrearCurso( CursoDTO cursoDTO, Stream? imagenStream = null, string? nombreImagen = null, Stream? videoStream = null, string? nombreVideo = null, List<IFormFile>? archivosRecursos = null) 
        {
            if (string.IsNullOrWhiteSpace(cursoDTO.NombreCurso))
                return OperationResult.Fail("El nombre del curso es obligatorio.");

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
            {
                cursoDTO.ImagenPortadaPath = await _service.UploadFile(imagenStream, nombreImagen, "portadas_imagen");
            }

            if (videoStream != null && !string.IsNullOrEmpty(nombreVideo))
            {
                cursoDTO.VideoPromocionalPath = await _service.UploadFile(videoStream, nombreVideo, "promocionales_video");
            }

            try
            {
                var resultado = await _repo.CreateCurso(cursoDTO);
                if (!resultado.Success) return resultado;

                int cursoId = Convert.ToInt32(resultado.Data);

                foreach (var rec in cursoDTO.Recursos ?? new List<RecursoDTO>())
                {
                    int moduloRecursoId = await _repo.ObtenerIdRelacion(cursoId, rec.ModuloId, rec.OrdenRecurso);
                    await ProcesarDetalleRecurso(moduloRecursoId, rec, archivosRecursos);
                }

                if (cursoDTO.InstructorIds?.Any() == true)
                {
                    foreach (var instructorId in cursoDTO.InstructorIds)
                    {
                        await _serviceNoti.CrearNotificacion(new CrearNotificacionDTO
                        {
                            UsuarioId = instructorId,
                            Tipo = "NOT_CAPACITACION_MASTER",
                            Mensaje = $"Fuiste asignado como instructor del curso \"{cursoDTO.NombreCurso}\".",
                            ReferenciaId = cursoId
                        });
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Error al crear el curso completo: " + ex.Message);
            }
        }

        public async Task<OperationResult> EditarCurso(CursoDTO cursoDTO, Stream? imagenStream = null, string? nombreImagen = null, Stream? videoStream = null, string? nombreVideo = null, List<IFormFile>? archivosRecursos = null)
        {
            if (cursoDTO.CursoId <= 0)
                return OperationResult.Fail("El ID del curso no es válido para edición.");

            if (string.IsNullOrWhiteSpace(cursoDTO.NombreCurso))
                return OperationResult.Fail("El nombre del curso es obligatorio.");

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
            {
                cursoDTO.ImagenPortadaPath = await _service.UploadFile(imagenStream, nombreImagen, "portadas_imagen");
            }

            if (videoStream != null && !string.IsNullOrEmpty(nombreVideo))
            {
                cursoDTO.VideoPromocionalPath = await _service.UploadFile(videoStream, nombreVideo, "promocionales_video");
            }

            try
            {
                var resultado = await _repo.UpdateCurso(cursoDTO);
                if (!resultado.Success) return resultado;

                var modulosActivos = cursoDTO.Modulos?.Select(m => m.OrdenModulo).ToHashSet() ?? new HashSet<int>();

                if (cursoDTO.Recursos != null)
                {
                    var recursosActivos = cursoDTO.Recursos
                        .Where(r => modulosActivos.Contains(r.ModuloId))
                        .ToList();

                    foreach (var rec in recursosActivos)
                    {
                        int moduloRecursoId = await _repo.ObtenerIdRelacion(
                            cursoDTO.CursoId, rec.ModuloId, rec.OrdenRecurso);

                        if (moduloRecursoId <= 0) continue;

                        bool esNuevo = rec.ModuloRecursoId == 0 || rec.ModuloRecursoId == null;

                        if (esNuevo)
                            await ProcesarDetalleRecurso(moduloRecursoId, rec, archivosRecursos);
                        else
                            await ActualizarDetalleRecurso(moduloRecursoId, rec, archivosRecursos);
                    }
                }

                if (cursoDTO.InstructorIds?.Any() == true)
                {
                    foreach (var instructorId in cursoDTO.InstructorIds)
                    {
                        await _serviceNoti.CrearNotificacion(new CrearNotificacionDTO
                        {
                            UsuarioId = instructorId,
                            Tipo = "NOT_CAPACITACION_MASTER",
                            Mensaje = $"Fuiste asignado como instructor del curso \"{cursoDTO.NombreCurso}\".",
                            ReferenciaId = cursoDTO.CursoId
                        });
                    }
                }

                return OperationResult.Ok(resultado, "Curso actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Error al editar el curso completo: " + ex.Message);
            }
        }

        public async Task<OperationResult<CursoDTO?>> ObtenerCursoId(int id)
        {
            var curso = await _repo.GetCursoById(id);

            if (curso == null) return OperationResult<CursoDTO?>.Fail("Curso no encontrado");

            if (!string.IsNullOrEmpty(curso.ImagenPortadaPath)) curso.ImagenPortadaPath = _service.GetPresignedUrl(curso.ImagenPortadaPath, 60);

            if (!string.IsNullOrEmpty(curso.VideoPromocionalPath)) curso.VideoPromocionalPath = _service.GetPresignedUrl(curso.VideoPromocionalPath, 120);

            if (curso.Recursos != null && curso.Recursos.Any())
            {
                foreach (var recurso in curso.Recursos)
                {
                    GenerarUrlsPresignadasRecurso(recurso);
                }
            }

            return OperationResult<CursoDTO?>.Ok(curso, "Curso obtenido correctamente");
        }

        public async Task<OperationResult<IEnumerable<ListarCursoDTO>>> ObtenerCursos(bool soloActivos = true)
        {
            var cursos = await _repo.GetCursos(soloActivos);

            var lista = (cursos ?? Enumerable.Empty<ListarCursoDTO>()).ToList();

            foreach (var curso in lista)
            {
                if (!string.IsNullOrEmpty(curso.ImagenPortadaPath))
                    curso.ImagenPortadaPath = _service.GetPresignedUrl(curso.ImagenPortadaPath, 60);

                if (!string.IsNullOrEmpty(curso.VideoPromocionalPath))
                    curso.VideoPromocionalPath = _service.GetPresignedUrl(curso.VideoPromocionalPath, 120);
            }

            return OperationResult<IEnumerable<ListarCursoDTO>>.Ok(lista, "Cursos obtenido correctamente");
        }


        private async Task ProcesarDetalleRecurso(int moduloRecursoId, RecursoDTO rec, List<IFormFile>? archivos = null)
        {
            if (rec.DataJson == null || rec.DataJson.Value.ValueKind == JsonValueKind.Null) return;

            var jsonRaw = rec.DataJson.Value.GetRawText();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            switch (rec.RecursoId)
            {
                case 1:
                    var eval = JsonSerializer.Deserialize<RecursoEvaluacionDTO>(jsonRaw, options);
                    if (eval == null) return;
                    eval.ModuloRecursoId = moduloRecursoId;
                    var archivosEval = archivos?
                        .Select(a => (stream: (Stream)a.OpenReadStream(), nombre: a.FileName))
                        .ToList();
                    await _reservice.Evaluacion.CrearEvaluacion(eval, archivosEval);
                    break;

                case 2:
                    var foro = JsonSerializer.Deserialize<RecursoForoDTO>(jsonRaw, options);
                    foro.ModuloRecursoId = moduloRecursoId;

                    if (foro.Archivos != null)
                        foreach (var a in foro.Archivos)
                            if (!string.IsNullOrEmpty(a.ArchivoPath) && a.ArchivoPath.StartsWith("http"))
                                a.ArchivoPath = _service.ExtraerKeyRelativo(a.ArchivoPath);

                    var archivosForoNuevos = archivos?
                        .Where(a => foro.Archivos != null &&
                            foro.Archivos.Any(af => string.IsNullOrEmpty(af.ArchivoPath) &&
                                Path.GetFileName(af.NombreArchivo) == Path.GetFileName(a.FileName)))
                        .Select(a => ((Stream)a.OpenReadStream(), a.FileName))
                        .ToList();

                    await _reservice.Foro.CrearForo(foro, archivosForoNuevos);
                    break;

                case 3:
                    var tarea = JsonSerializer.Deserialize<RecursoTareaDTO>(jsonRaw, options);
                    tarea.ModuloRecursoId = moduloRecursoId;

                    if (tarea.Archivos != null)
                        foreach (var a in tarea.Archivos)
                            if (!string.IsNullOrEmpty(a.ArchivoPath) && a.ArchivoPath.StartsWith("http"))
                                a.ArchivoPath = _service.ExtraerKeyRelativo(a.ArchivoPath);

                    var archivosTareaNuevos = archivos?
                        .Where(a => tarea.Archivos != null &&
                            tarea.Archivos.Any(at => string.IsNullOrEmpty(at.ArchivoPath) &&
                                Path.GetFileName(at.NombreArchivo) == Path.GetFileName(a.FileName)))
                        .Select(a => ((Stream)a.OpenReadStream(), a.FileName))
                        .ToList();

                    await _reservice.Tarea.CrearTarea(tarea, archivosTareaNuevos);
                    break;

                case 4:
                    var video = JsonSerializer.Deserialize<RecursoVideoDTO>(jsonRaw, options);
                    video.ModuloRecursoId = moduloRecursoId;
                    var archivoVideo = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(video.VideoPath));
                    if (archivoVideo == null && !string.IsNullOrEmpty(video.VideoPath) && video.VideoPath.StartsWith("http"))
                        video.VideoPath = _service.ExtraerKeyRelativo(video.VideoPath);
                    await _reservice.Video.CrearVideo(video, archivoVideo?.OpenReadStream(), archivoVideo?.FileName);
                    break;

                case 5:
                    var lectura = JsonSerializer.Deserialize<RecursoLecturaDTO>(jsonRaw, options);
                    lectura.ModuloRecursoId = moduloRecursoId;
                    var archivoLectura = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(lectura.ArchivoPDFPath));
                    if (archivoLectura == null && !string.IsNullOrEmpty(lectura.ArchivoPDFPath) && lectura.ArchivoPDFPath.StartsWith("http"))
                        lectura.ArchivoPDFPath = _service.ExtraerKeyRelativo(lectura.ArchivoPDFPath);

                    var archivosAdjuntosLectura = archivos?
                        .Where(a => lectura.ArchivosAdjuntos != null &&
                            lectura.ArchivosAdjuntos.Any(adj =>
                                Path.GetFileName(adj.NombreArchivo) == Path.GetFileName(a.FileName)))
                        .Select(a => (stream: (Stream)a.OpenReadStream(), nombre: a.FileName))
                        .ToList();

                    await _reservice.Lectura.CrearLectura(
                        lectura,
                        archivoLectura?.OpenReadStream(),
                        archivoLectura?.FileName,
                        archivosAdjuntosLectura);
                    break;

                case 6: 
                    var zoom = JsonSerializer.Deserialize<RecursoZoomDTO>(jsonRaw, options);
                    zoom.ModuloRecursoId = moduloRecursoId;
                    await _reservice.Zoom.CrearZoom(zoom);
                    break;

                case 7: 
                    var embebido = JsonSerializer.Deserialize<RecursoEmbebidoDTO>(jsonRaw, options);
                    embebido.ModuloRecursoId = moduloRecursoId;
                    await _reservice.Embebido.CrearEmbebido(embebido);
                    break;

                case 8:
                    var scorm = JsonSerializer.Deserialize<RecursoScormDTO>(jsonRaw, options);
                    scorm.ModuloRecursoId = moduloRecursoId;

                    Console.WriteLine($"[SCORM-CREAR] ModuloRecursoId: {moduloRecursoId}");
                    Console.WriteLine($"[SCORM-CREAR] jsonRaw: {jsonRaw}");
                    Console.WriteLine($"[SCORM-CREAR] TipoCalificacionId: {scorm.TipoCalificacionId}");
                    Console.WriteLine($"[SCORM-CREAR] AgregarPonderacion: {scorm.AgregarPonderacion}");
                    Console.WriteLine($"[SCORM-CREAR] ArchivoPath: {scorm.ArchivoPath}");
                    Console.WriteLine($"[SCORM-CREAR] NombreArchivo: {scorm.NombreArchivo}");

                    var archivoScorm = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(scorm.ArchivoPath));

                    Console.WriteLine($"archivoScorm encontrado: {archivoScorm?.FileName ?? "NULL"}");

                    if (archivoScorm == null && !string.IsNullOrEmpty(scorm.ArchivoPath) && scorm.ArchivoPath.StartsWith("http"))
                        scorm.ArchivoPath = _service.ExtraerKeyRelativo(scorm.ArchivoPath);
                    await _reservice.Scorm.CrearScorm(scorm, archivoScorm?.OpenReadStream(), archivoScorm?.FileName);
                    break;

                case 9:
                    var encuesta = JsonSerializer.Deserialize<RecursoEncuestaDTO>(jsonRaw, options);
                    encuesta.ModuloRecursoId = moduloRecursoId;

                    if ((encuesta.BancasIds == null || encuesta.BancasIds.Count == 0)
                        && rec.DataJson.HasValue
                        && rec.DataJson.Value.TryGetProperty("BancasPreguntas", out var bancasElement)
                        && bancasElement.ValueKind != JsonValueKind.Null)
                    {
                        encuesta.BancasIds = JsonSerializer.Deserialize<List<BancaPreguntaSeleccionadaDTO>>(
                            bancasElement.GetRawText(), options);
                    }

                    await _reservice.Encuesta.CrearEncuesta(encuesta);
                    break;

                case 10: 
                    var sesion = JsonSerializer.Deserialize<RecursoSesionPresencialDTO>(jsonRaw, options);
                    sesion.ModuloRecursoId = moduloRecursoId;
                    await _reservice.Sesion.CrearSesionPresencial(sesion);
                    break;

                case 11: 
                    var evalPresencial = JsonSerializer.Deserialize<RecursoEvaluacionPresencialDTO>(jsonRaw, options);
                    evalPresencial.ModuloRecursoId = moduloRecursoId;

                    Console.WriteLine($"[EVAL-PRESENCIAL] ModuloRecursoId: {moduloRecursoId}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] jsonRaw: {jsonRaw}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] AgregarPonderacion: {evalPresencial.AgregarPonderacion}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] ColaboradorSolicitarRevision: {evalPresencial.ColaboradorSolicitarRevision}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] TipoCalificacionId: {evalPresencial.TipoCalificacionId}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] Rubricas count: {evalPresencial.Rubricas?.Count}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] Criterios count: {evalPresencial.Criterios?.Count}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] Calificaciones count: {evalPresencial.Calificaciones?.Count}");

                    await _reservice.EvaluacionPrecencial.CrearEvaluacionPresencial(evalPresencial);
                    break;

                case 12:
                    var videoPregunta = JsonSerializer.Deserialize<RecursoVideoPreguntaDTO>(jsonRaw, options);
                    if (videoPregunta == null) return;

                    var archivoVideoPregunta = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(videoPregunta.VideoPath));

                    if (archivoVideoPregunta != null)
                    {
                        using var stream = archivoVideoPregunta.OpenReadStream();
                        videoPregunta.VideoPath = await _service.UploadFile(
                            stream, archivoVideoPregunta.FileName, "archivo-video");
                    }
                    else if (!string.IsNullOrEmpty(videoPregunta.VideoPath) && videoPregunta.VideoPath.StartsWith("http"))
                    {
                        videoPregunta.VideoPath = _service.ExtraerKeyRelativo(videoPregunta.VideoPath);
                    }

                    var videoBaseDto = new RecursoVideoDTO
                    {
                        ModuloRecursoId = moduloRecursoId,
                        VideoPath = videoPregunta.VideoPath,
                        VideoLink = videoPregunta.VideoLink,
                        TipoSubida = videoPregunta.TipoSubida,
                        HacerVisibleDashboard = videoPregunta.HacerVisibleDashboard ?? false
                    };

                    await _reservice.Video.CrearVideo(videoBaseDto, null, null);

                    foreach (var pregunta in videoPregunta.Preguntas ?? new List<PreguntaVideoDTO>())
                    {
                        pregunta.ModuloRecursoId = moduloRecursoId;
                        pregunta.VideoPath = videoPregunta.VideoPath ?? "";
                        await _reservice.Pregunta.CrearPreguntaVideo(pregunta);
                    }
                    break;

                default:
                    throw new Exception($"El tipo de recurso {rec.RecursoId} no está soportado en la creación masiva.");
            }
        }

        private void GenerarUrlsPresignadasRecurso(RecursoDTO recurso)
        {
            if (recurso.DataJson == null || recurso.DataJson.Value.ValueKind == JsonValueKind.Null) return;

            var jsonRaw = recurso.DataJson.Value.GetRawText();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var jsonActualizado = recurso.RecursoId switch
            {
                1 => _reservice.Evaluacion.GenerarPresignedDataJson(jsonRaw),
                2 => _reservice.Foro.GenerarPresignedDataJson(jsonRaw),
                3 => _reservice.Tarea.GenerarPresignedDataJson(jsonRaw),
                4 => _reservice.Video.GenerarPresignedDataJson(jsonRaw),
                5 => _reservice.Lectura.GenerarPresignedDataJson(jsonRaw),
                8 => _reservice.Scorm.GenerarPresignedDataJson(jsonRaw),
                12 => _reservice.Pregunta.GenerarPresignedDataJson(jsonRaw),
                _ => jsonRaw  
            };

            if (jsonActualizado != jsonRaw)
                recurso.DataJson = JsonSerializer.Deserialize<JsonElement>(jsonActualizado, options);
        }

        public async Task<OperationResult> DuplicarCurso(int cursoId)
        {
            return await _repo.DuplicarCurso(cursoId);
        }

        public async Task<OperationResult> AgregarRecursosCurso(int cursoId, List<ModuloDTO> modulos, List<RecursoDTO> recursos, List<IFormFile>? archivosRecursos = null)
        {
            try
            {
                var idsCreados = await _repo.AgregarRelacionesModuloRecurso(cursoId, modulos, recursos);

                if (!idsCreados.Any())
                    return OperationResult.Fail("No se pudieron crear las relaciones.");

                foreach (var ids in idsCreados)
                {
                    var rec = recursos.FirstOrDefault(r =>
                        r.ModuloId == ids.ModuloId &&      
                        r.OrdenRecurso == ids.OrdenRecurso);

                    if (rec == null) continue;

                    await ProcesarDetalleRecurso(ids.ModuloRecursoId, rec, archivosRecursos);
                }

                return OperationResult.Ok(idsCreados, "Recursos agregados correctamente");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Error al agregar recursos: " + ex.Message);
            }
        }

        public async Task<OperationResult> ActualizarPonderacion(int cursoId, List<PonderacionItemDTO> ponderaciones, decimal? calificacion = null, int? requisitoAvance = null)
        {
            try
            {
                await _repo.ActualizarPonderacionRecursos(ponderaciones);

                if (calificacion.HasValue || requisitoAvance.HasValue)
                    await _repo.ActualizarCriterioCurso(cursoId, calificacion, requisitoAvance);

                return OperationResult.Ok("Ponderación actualizada correctamente");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Error al actualizar ponderación: " + ex.Message);
            }
        }

        private async Task ActualizarDetalleRecurso(int moduloRecursoId, RecursoDTO rec, List<IFormFile>? archivos = null)
        {
            if (rec.DataJson == null || rec.DataJson.Value.ValueKind == JsonValueKind.Null) return;

            var jsonRaw = rec.DataJson.Value.GetRawText();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            switch (rec.RecursoId)
            {
                case 1:
                    var eval = JsonSerializer.Deserialize<RecursoEvaluacionDTO>(jsonRaw, options);
                    if (eval == null) return;
                    eval.ModuloRecursoId = moduloRecursoId;
                    var archivosEval = archivos?
                        .Select(a => (stream: (Stream)a.OpenReadStream(), nombre: a.FileName))
                        .ToList();
                    await _reservice.Evaluacion.CrearEvaluacion(eval, archivosEval);
                    break;

                case 2:
                    var foro = JsonSerializer.Deserialize<RecursoForoDTO>(jsonRaw, options);
                    foro.ModuloRecursoId = moduloRecursoId;

                    if (foro.Archivos != null)
                        foreach (var a in foro.Archivos)
                            if (!string.IsNullOrEmpty(a.ArchivoPath) && a.ArchivoPath.StartsWith("http"))
                                a.ArchivoPath = _service.ExtraerKeyRelativo(a.ArchivoPath);

                    var archivosForoNuevos = archivos?
                        .Where(a => foro.Archivos != null &&
                            foro.Archivos.Any(af => string.IsNullOrEmpty(af.ArchivoPath) &&  
                                Path.GetFileName(af.NombreArchivo) == Path.GetFileName(a.FileName)))
                        .Select(a => ((Stream)a.OpenReadStream(), a.FileName))
                        .ToList();

                    await _reservice.Foro.CrearForo(foro, archivosForoNuevos);
                    break;

                case 3:
                    var tarea = JsonSerializer.Deserialize<RecursoTareaDTO>(jsonRaw, options);
                    tarea.ModuloRecursoId = moduloRecursoId;

                    if (tarea.Archivos != null)
                        foreach (var a in tarea.Archivos)
                            if (!string.IsNullOrEmpty(a.ArchivoPath) && a.ArchivoPath.StartsWith("http"))
                                a.ArchivoPath = _service.ExtraerKeyRelativo(a.ArchivoPath);

                    var archivosTareaNuevos = archivos?
                        .Where(a => tarea.Archivos != null &&
                            tarea.Archivos.Any(at => string.IsNullOrEmpty(at.ArchivoPath) && 
                                Path.GetFileName(at.NombreArchivo) == Path.GetFileName(a.FileName)))
                        .Select(a => ((Stream)a.OpenReadStream(), a.FileName))
                        .ToList();

                    await _reservice.Tarea.CrearTarea(tarea, archivosTareaNuevos);
                    break;

                case 4:
                    var video = JsonSerializer.Deserialize<RecursoVideoDTO>(jsonRaw, options);
                    video.ModuloRecursoId = moduloRecursoId;
                    var archivoVideo = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(video.VideoPath));
                    if (archivoVideo == null && !string.IsNullOrEmpty(video.VideoPath) && video.VideoPath.StartsWith("http"))
                        video.VideoPath = _service.ExtraerKeyRelativo(video.VideoPath);
                    await _reservice.Video.CrearVideo(video, archivoVideo?.OpenReadStream(), archivoVideo?.FileName);
                    break;

                case 5:
                    var lectura = JsonSerializer.Deserialize<RecursoLecturaDTO>(jsonRaw, options);
                    lectura.ModuloRecursoId = moduloRecursoId;
                    var archivoLectura = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(lectura.ArchivoPDFPath));
                    if (archivoLectura == null && !string.IsNullOrEmpty(lectura.ArchivoPDFPath) && lectura.ArchivoPDFPath.StartsWith("http"))
                        lectura.ArchivoPDFPath = _service.ExtraerKeyRelativo(lectura.ArchivoPDFPath);

                    var archivosAdjuntosLectura = archivos?
                        .Where(a => lectura.ArchivosAdjuntos != null &&
                            lectura.ArchivosAdjuntos.Any(adj =>
                                Path.GetFileName(adj.NombreArchivo) == Path.GetFileName(a.FileName)))
                        .Select(a => (stream: (Stream)a.OpenReadStream(), nombre: a.FileName))
                        .ToList();

                    await _reservice.Lectura.CrearLectura(
                        lectura,
                        archivoLectura?.OpenReadStream(),
                        archivoLectura?.FileName,
                        archivosAdjuntosLectura);
                    break;

                case 6:
                    var zoom = JsonSerializer.Deserialize<RecursoZoomDTO>(jsonRaw, options);
                    zoom.ModuloRecursoId = moduloRecursoId;
                    await _reservice.Zoom.CrearZoom(zoom);
                    break;

                case 7:
                    var embebido = JsonSerializer.Deserialize<RecursoEmbebidoDTO>(jsonRaw, options);
                    embebido.ModuloRecursoId = moduloRecursoId;
                    await _reservice.Embebido.CrearEmbebido(embebido);
                    break;

                case 8:
                    var scorm = JsonSerializer.Deserialize<RecursoScormDTO>(jsonRaw, options);
                    scorm.ModuloRecursoId = moduloRecursoId;

                    Console.WriteLine($"[SCORM-ACTUALIZAR] ModuloRecursoId: {moduloRecursoId}");
                    Console.WriteLine($"[SCORM-ACTUALIZAR] jsonRaw: {jsonRaw}");
                    Console.WriteLine($"[SCORM-ACTUALIZAR] TipoCalificacionId: {scorm.TipoCalificacionId}");
                    Console.WriteLine($"[SCORM-ACTUALIZAR] AgregarPonderacion: {scorm.AgregarPonderacion}");

                    var archivoScorm = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(scorm.ArchivoPath));
                    if (archivoScorm == null && !string.IsNullOrEmpty(scorm.ArchivoPath) && scorm.ArchivoPath.StartsWith("http"))
                        scorm.ArchivoPath = _service.ExtraerKeyRelativo(scorm.ArchivoPath);
                    await _reservice.Scorm.CrearScorm(scorm, archivoScorm?.OpenReadStream(), archivoScorm?.FileName);
                    break;

                case 9:
                    var encuesta = JsonSerializer.Deserialize<RecursoEncuestaDTO>(jsonRaw, options);
                    encuesta.ModuloRecursoId = moduloRecursoId;

                    if ((encuesta.BancasIds == null || encuesta.BancasIds.Count == 0)
                        && rec.DataJson.HasValue
                        && rec.DataJson.Value.TryGetProperty("BancasPreguntas", out var bancasElement)
                        && bancasElement.ValueKind != JsonValueKind.Null)
                    {
                        encuesta.BancasIds = JsonSerializer.Deserialize<List<BancaPreguntaSeleccionadaDTO>>(
                            bancasElement.GetRawText(), options);
                    }

                    await _reservice.Encuesta.CrearEncuesta(encuesta);
                    break;

                case 10:
                    var sesion = JsonSerializer.Deserialize<RecursoSesionPresencialDTO>(jsonRaw, options);
                    sesion.ModuloRecursoId = moduloRecursoId;
                    await _reservice.Sesion.CrearSesionPresencial(sesion);
                    break;

                case 11:
                    var evalPresencial = JsonSerializer.Deserialize<RecursoEvaluacionPresencialDTO>(jsonRaw, options);
                    evalPresencial.ModuloRecursoId = moduloRecursoId;

                    Console.WriteLine($"[EVAL-PRESENCIAL] ModuloRecursoId: {moduloRecursoId}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] jsonRaw: {jsonRaw}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] AgregarPonderacion: {evalPresencial.AgregarPonderacion}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] ColaboradorSolicitarRevision: {evalPresencial.ColaboradorSolicitarRevision}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] TipoCalificacionId: {evalPresencial.TipoCalificacionId}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] Rubricas count: {evalPresencial.Rubricas?.Count}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] Criterios count: {evalPresencial.Criterios?.Count}");
                    Console.WriteLine($"[EVAL-PRESENCIAL] Calificaciones count: {evalPresencial.Calificaciones?.Count}");

                    await _reservice.EvaluacionPrecencial.CrearEvaluacionPresencial(evalPresencial);
                    break;

                case 12:
                    var videoPregunta = JsonSerializer.Deserialize<RecursoVideoPreguntaDTO>(jsonRaw, options);
                    if (videoPregunta == null) return;

                    var archivoVideoPregunta = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.FileName) == Path.GetFileName(videoPregunta.VideoPath));

                    if (archivoVideoPregunta != null)
                    {
                        using var stream = archivoVideoPregunta.OpenReadStream();
                        videoPregunta.VideoPath = await _service.UploadFile(
                            stream, archivoVideoPregunta.FileName, "archivo-video");
                    }
                    else if (!string.IsNullOrEmpty(videoPregunta.VideoPath) && videoPregunta.VideoPath.StartsWith("http"))
                    {
                        videoPregunta.VideoPath = _service.ExtraerKeyRelativo(videoPregunta.VideoPath);
                    }

                    var videoBaseDto = new RecursoVideoDTO
                    {
                        ModuloRecursoId = moduloRecursoId,
                        VideoPath = videoPregunta.VideoPath,
                        VideoLink = videoPregunta.VideoLink,
                        TipoSubida = videoPregunta.TipoSubida,
                        HacerVisibleDashboard = videoPregunta.HacerVisibleDashboard ?? false
                    };

                    await _reservice.Video.CrearVideo(videoBaseDto, null, null); 
                    await _reservice.Pregunta.ActualizarPreguntasVideo(moduloRecursoId, videoPregunta.Preguntas ?? new List<PreguntaVideoDTO>()); 
                    break;

                default:
                    throw new Exception($"El tipo de recurso {rec.RecursoId} no está soportado en la actualización.");
            }
        }

        public async Task<OperationResult> CambiarEstadoCurso(int cursoId, int adminId, string accion, string? motivo = null)
        {
            var accionesValidas = new[] { "PUBLICAR", "DESPUBLICAR", "ARCHIVAR", "RESTAURAR" };
            if (!accionesValidas.Contains(accion.ToUpper()))
                return OperationResult.Fail($"Acción '{accion}' no válida. Use: PUBLICAR, DESPUBLICAR, ARCHIVAR o RESTAURAR.");

            return await _repo.CambiarEstadoCurso(cursoId, adminId, accion.ToUpper(), motivo);
        }

        public async Task<OperationResult> EliminarCurso(int cursoId, int adminId, string? motivo = null)
        {
            if (cursoId <= 0)
                return OperationResult.Fail("El ID del curso no es válido.");

            if (adminId <= 0)
                return OperationResult.Fail("El ID del administrador no es válido.");

            return await _repo.EliminarCurso(cursoId, adminId, motivo);
        }
    }
}
