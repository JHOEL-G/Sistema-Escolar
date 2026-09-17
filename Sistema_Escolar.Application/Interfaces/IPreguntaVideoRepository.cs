using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IPreguntaVideoRepository
    {
        Task<PreguntaVideoDTO> GetPorId(int id);

        Task<OperationResult> CreateVideoPregunta(PreguntaVideoDTO dto);
        Task<OperationResult> UpdatePreguntasVideo(int moduloRecursoId, List<PreguntaVideoDTO> preguntas);
    }
}
