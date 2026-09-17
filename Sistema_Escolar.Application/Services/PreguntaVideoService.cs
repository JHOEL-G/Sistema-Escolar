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
    public class PreguntaVideoService : IPreguntaVideoService
    {
        private readonly IPreguntaVideoRepository _repo;
        private readonly IFileStorageService _service;

        public PreguntaVideoService (IPreguntaVideoRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearPreguntaVideo(PreguntaVideoDTO dto, Stream? videoStream = null, string? nombreVideo = null)
        {
            if (videoStream != null && !string.IsNullOrEmpty(nombreVideo))
                dto.VideoPath = await _service.UploadFile(videoStream, nombreVideo, "video-pregunta");
            else if (!string.IsNullOrEmpty(dto.VideoPath) && dto.VideoPath.StartsWith("http"))
                dto.VideoPath = _service.ExtraerKeyRelativo(dto.VideoPath);

            return await _repo.CreateVideoPregunta(dto);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var videoPregunta = JsonSerializer.Deserialize<RecursoVideoPreguntaDTO>(jsonRaw, options);
            if (videoPregunta == null || string.IsNullOrEmpty(videoPregunta.VideoPath)) return jsonRaw;

            videoPregunta.VideoPath = _service.GetPresignedUrl(videoPregunta.VideoPath, 1000);
            return JsonSerializer.Serialize(videoPregunta, options);
        }

        public async Task<OperationResult<PreguntaVideoDTO?>> ObtenerPreguntasPorId(int id)
        {
            var resultado = await _repo.GetPorId(id);

            if (resultado == null)
                return OperationResult<PreguntaVideoDTO?>.Fail("Pregunta de video no encontrada");

            if (!string.IsNullOrEmpty(resultado.VideoPath))
            {
                resultado.VideoPath = _service.GetPresignedUrl(resultado.VideoPath, 10080);
            }

            return OperationResult<PreguntaVideoDTO?>.Ok(resultado, "Información obtenida correctamente");
        }

        public async Task<OperationResult> ActualizarPreguntasVideo(int moduloRecursoId, List<PreguntaVideoDTO> preguntas)
        {
            foreach (var pregunta in preguntas)
            {
                if (!string.IsNullOrEmpty(pregunta.VideoPath) && pregunta.VideoPath.StartsWith("http"))
                    pregunta.VideoPath = _service.ExtraerKeyRelativo(pregunta.VideoPath);
            }

            return await _repo.UpdatePreguntasVideo(moduloRecursoId, preguntas);
        }
    }
}
