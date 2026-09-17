using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IScormService
    {
        Task<OperationResult> CrearScorm(RecursoScormDTO dTO, Stream? archivoStream = null, string? nombreArchivo = null);

        string GenerarPresignedDataJson(string jsonRaw);
    }
}
