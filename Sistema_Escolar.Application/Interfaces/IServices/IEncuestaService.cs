using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IEncuestaService
    {
        Task<OperationResult> CrearEncuesta(RecursoEncuestaDTO dTO);
    }
}
