using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface ITemaRepository
    {
        Task<IEnumerable<TtemaDTO>> GetTtemas();
        Task<OperationResult> CreateTema(TtemaDTO ttemaDTO);
        Task<IEnumerable<TtemaDTO?>> GetTtemaId(int id);
        Task<OperationResult> UpdateTema(TtemaDTO ttemaDTO);
    }
}
