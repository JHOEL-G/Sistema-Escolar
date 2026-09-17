using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRecursoForoRepository
    {
        Task<OperationResult> CreateRecursoForo(RecursoForoDTO dto);
        Task<OperationResult> PublicarForo(PublicarForoDTO dto);
        Task<OperationResult> GetForosPublicado(int foro);
    }
}
