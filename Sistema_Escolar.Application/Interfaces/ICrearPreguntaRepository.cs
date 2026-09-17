using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface ICrearPreguntaRepository
    {
        Task<OperationResult> CreatePregunta(BancaPreguntaDTO dto);
        Task<IEnumerable<BancaPreguntaDTO>> GetPregunta();
        Task<BancaPreguntaDTO?> GetPreguntaById(int bancaId);
        Task<OperationResult> UpdatePregunta(int bancaId, BancaPreguntaDTO dto);
    }
}
