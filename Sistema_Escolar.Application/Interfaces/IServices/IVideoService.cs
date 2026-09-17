using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IVideoService
    {
        Task<OperationResult> CrearVideo(RecursoVideoDTO dTO, Stream? archivoStream = null, string? nombreArchivo = null);

        string GenerarPresignedDataJson(string jsonRaw);
    }
}
