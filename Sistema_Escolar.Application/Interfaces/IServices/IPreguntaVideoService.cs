using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IPreguntaVideoService
    {
        Task<OperationResult> CrearPreguntaVideo(PreguntaVideoDTO dto, Stream? videoStream = null, string? nombreVideo = null);

        Task<OperationResult<PreguntaVideoDTO?>> ObtenerPreguntasPorId(int id);

        string GenerarPresignedDataJson(string jsonRaw);
        Task<OperationResult> ActualizarPreguntasVideo(int moduloRecursoId, List<PreguntaVideoDTO> preguntas);
    }
}
