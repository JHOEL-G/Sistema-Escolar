using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IEvaluacionService
    {
        Task<OperationResult> ActualizarEvaluacion(RecursoEvaluacionDTO dTO, List<(Stream stream, string nombre)>? archivos = null);
        Task<OperationResult> CrearEvaluacion(RecursoEvaluacionDTO dTO, List<(Stream stream, string nombre)>? archivos = null);

        string GenerarPresignedDataJson(string jsonRaw);
    }
}
