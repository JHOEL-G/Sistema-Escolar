using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRecursoScormRepository
    {
        Task<OperationResult> CreateRecursoScorm(RecursoScormDTO dto);
    }
}
