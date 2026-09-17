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
    public class ScormService : IScormService
    {
        private readonly IRecursoScormRepository _repo;
        private readonly IFileStorageService _service;

        public ScormService (IRecursoScormRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearScorm(RecursoScormDTO dTO, Stream? archivoStream = null, string? nombreArchivo = null)
        {
            if (archivoStream != null && !string.IsNullOrEmpty(nombreArchivo))
            {
                dTO.ArchivoPath = await _service.UploadFile(archivoStream, nombreArchivo, "archivo-scorm");
            }
            else if (!string.IsNullOrEmpty(dTO.ArchivoPath) && dTO.ArchivoPath.StartsWith("http"))
            {
                dTO.ArchivoPath = _service.ExtraerKeyRelativo(dTO.ArchivoPath);
            }

            return await _repo.CreateRecursoScorm(dTO);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var scorm = JsonSerializer.Deserialize<RecursoScormDTO>(jsonRaw, options);
            if (scorm == null || string.IsNullOrEmpty(scorm.ArchivoPath)) return jsonRaw;

            scorm.ArchivoPath = _service.GetPresignedUrl(scorm.ArchivoPath, 180);
            return JsonSerializer.Serialize(scorm, options);
        }
    }
}
