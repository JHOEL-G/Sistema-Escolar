using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IRecursoEvaluacionRepository _repo;
        private readonly IFileStorageService _service;

        public EvaluacionService (IRecursoEvaluacionRepository repo, IFileStorageService service)
        {
            _repo = repo; 
            _service = service;
        }

        public async Task<OperationResult> CrearEvaluacion(RecursoEvaluacionDTO dTO, List<(Stream stream, string nombre)>? archivos = null)
        {
            if ((dTO.BancasIds == null || dTO.BancasIds.Count == 0) && dTO.BancasPreguntas?.Count > 0)
            {
                dTO.BancasIds = dTO.BancasPreguntas;
            }

            if (archivos != null && archivos.Any())
            {
                foreach (var pregunta in dTO.Preguntas ?? new List<PreguntaManualDTO>())
                {
                    var archivoP = archivos.FirstOrDefault(a =>
                        Path.GetFileName(a.nombre) == Path.GetFileName(pregunta.ImagenPregunta));

                    if (archivoP != default)
                    {
                        pregunta.ImagenPregunta = await _service.UploadFile(
                            archivoP.stream, archivoP.nombre, "imagen-pregunta");
                    }

                    foreach (var opcion in pregunta.Opciones ?? new List<OpcionManualDTO>())
                    {
                        var archivoO = archivos.FirstOrDefault(a =>
                            Path.GetFileName(a.nombre) == Path.GetFileName(opcion.ImagenOpcion));

                        if (archivoO != default)
                        {
                            opcion.ImagenOpcion = await _service.UploadFile(
                                archivoO.stream, archivoO.nombre, "imagen-opcion");
                        }
                    }
                }
            }

            return await _repo.CreateRecursoEvaluacion(dTO);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString 
            };
            var eval = JsonSerializer.Deserialize<RecursoEvaluacionDTO>(jsonRaw, options);
            if (eval == null) return jsonRaw;

            bool modificado = false;

            foreach (var pregunta in eval.Preguntas ?? new List<PreguntaManualDTO>())
            {
                if (!string.IsNullOrEmpty(pregunta.ImagenPregunta))
                {
                    pregunta.ImagenPregunta = _service.GetPresignedUrl(pregunta.ImagenPregunta, 120);
                    modificado = true;
                }

                foreach (var opcion in pregunta.Opciones ?? new List<OpcionManualDTO>())
                {
                    if (!string.IsNullOrEmpty(opcion.ImagenOpcion))
                    {
                        opcion.ImagenOpcion = _service.GetPresignedUrl(opcion.ImagenOpcion, 120);
                        modificado = true;
                    }
                }
            }

            return modificado ? JsonSerializer.Serialize(eval, options) : jsonRaw;
        }

        public async Task<OperationResult> ActualizarEvaluacion(RecursoEvaluacionDTO dTO, List<(Stream stream, string nombre)>? archivos = null)
        {
            if ((dTO.BancasIds == null || dTO.BancasIds.Count == 0) && dTO.BancasPreguntas?.Count > 0)
                dTO.BancasIds = dTO.BancasPreguntas;

            foreach (var pregunta in dTO.Preguntas ?? new List<PreguntaManualDTO>())
            {
                var archivoP = archivos?.FirstOrDefault(a =>
                    Path.GetFileName(a.nombre) == Path.GetFileName(pregunta.ImagenPregunta));

                if (archivoP.HasValue && archivoP.Value != default)  
                    pregunta.ImagenPregunta = await _service.UploadFile(
                        archivoP.Value.stream, archivoP.Value.nombre, "imagen-pregunta");
                else if (!string.IsNullOrEmpty(pregunta.ImagenPregunta) && pregunta.ImagenPregunta.StartsWith("http"))
                    pregunta.ImagenPregunta = _service.ExtraerKeyRelativo(pregunta.ImagenPregunta);

                foreach (var opcion in pregunta.Opciones ?? new List<OpcionManualDTO>())
                {
                    var archivoO = archivos?.FirstOrDefault(a =>
                        Path.GetFileName(a.nombre) == Path.GetFileName(opcion.ImagenOpcion));

                    if (archivoO.HasValue && archivoO.Value != default)  
                        opcion.ImagenOpcion = await _service.UploadFile(
                            archivoO.Value.stream, archivoO.Value.nombre, "imagen-opcion");
                    else if (!string.IsNullOrEmpty(opcion.ImagenOpcion) && opcion.ImagenOpcion.StartsWith("http"))
                        opcion.ImagenOpcion = _service.ExtraerKeyRelativo(opcion.ImagenOpcion);
                }
            }

            return await _repo.UpdateRecursoEvaluacion(dTO);
        }
    }
}
