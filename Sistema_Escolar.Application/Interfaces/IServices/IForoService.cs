using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IForoService
    {
        Task<OperationResult> CrearForo(RecursoForoDTO dto, List<(Stream Stream, string NombreArchivo)>? archivos = null);
        string GenerarPresignedDataJson(string jsonRaw);
        Task<OperationResult> ListarForoPublicado(int foro);
        Task<OperationResult> PublicarForo(PublicarForoDTO dto, Stream? archivoStream = null, string? nombreArchivo = null);
    }
}
