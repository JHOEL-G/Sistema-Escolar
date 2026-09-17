using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface ILecturaService
    {
        Task<OperationResult> CrearLectura(RecursoLecturaDTO dTO, Stream? archivoStream = null, string? nombreArchivo = null, List<(Stream stream, string nombre)>? archivosAdjuntos = null);
        string GenerarPresignedDataJson(string jsonRaw);
    }
}
