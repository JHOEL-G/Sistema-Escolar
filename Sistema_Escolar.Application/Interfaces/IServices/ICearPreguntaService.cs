using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface ICearPreguntaService
    {
        Task<OperationResult> CearPregunta(BancaPreguntaDTO dto);

        Task<OperationResult<IEnumerable<BancaPreguntaDTO>>> ObtenerPreguntas();

        Task<OperationResult<BancaPreguntaDTO?>> ObtenerPreguntaPorId(int bancaId);

        Task<OperationResult> ActualizarPregunta(int bancaId, BancaPreguntaDTO dto);
    }
}
