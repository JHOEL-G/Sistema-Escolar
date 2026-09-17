using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRecursoTareaRepository
    {
        Task<OperationResult> CreateRecursoTarea(RecursoTareaDTO dto);
        Task<OperationResult> EntregarTarea(EntregarTareaDTO dto);
        Task<OperationResult> GetTareaEntregados(int tarea, int usuario);
    }
}
