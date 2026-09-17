using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRecursoEvaluacionRepository
    {
        Task<OperationResult> CreateRecursoEvaluacion(RecursoEvaluacionDTO dto);
        Task<OperationResult> UpdateRecursoEvaluacion(RecursoEvaluacionDTO dto);
    }
}
