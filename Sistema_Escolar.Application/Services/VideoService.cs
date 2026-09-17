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
    public class VideoService : IVideoService
    {
        private readonly IRecursoVideoRepository _repo;
        private readonly IFileStorageService _service;

        public VideoService (IRecursoVideoRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearVideo(RecursoVideoDTO dTO, Stream? archivoStream = null, string? nombreArchivo = null)
        {
            if (archivoStream != null && !string.IsNullOrEmpty(nombreArchivo))
                dTO.VideoPath = await _service.UploadFile(archivoStream, nombreArchivo, "archivo-video");
            else if (!string.IsNullOrEmpty(dTO.VideoPath) && dTO.VideoPath.StartsWith("http"))
                dTO.VideoPath = _service.ExtraerKeyRelativo(dTO.VideoPath);

            return await _repo.CreateRecursoVideo(dTO);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var video = JsonSerializer.Deserialize<RecursoVideoDTO>(jsonRaw, options);
            if (video == null || string.IsNullOrEmpty(video.VideoPath)) return jsonRaw;

            video.VideoPath = _service.GetPresignedUrl(video.VideoPath, 10080);
            return JsonSerializer.Serialize(video, options);
        }
    }
}
