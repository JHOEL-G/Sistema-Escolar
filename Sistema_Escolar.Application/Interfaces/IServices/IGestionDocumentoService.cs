using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IGestionDocumentoService
    {
        Task<OperationResult> CrearCarpeta(CarpetaInsertarDTO dto);
        Task<OperationResult> ActualizarCarpeta(CarpetaActualizarDTO dto);
        Task<OperationResult> EliminarCarpeta(int exploradorId);
        Task<OperationResult<List<CarpetaResponseDTO>>> ListarArbolCarpetas();


        Task<OperationResult> CrearDocumento(DocumentoInsertarDTO dto, Stream? documentoStream = null, string? nombreDocumento = null);
        Task<OperationResult> ActualizarDocumento(DocumentoActualizarDTO dto);
        Task<OperationResult> EliminarDocumento(int gestionId);
        Task<OperationResult<List<DocumentoResponseDTO>>> ListarDocumentosPorCarpeta(int exploradorId);

    }
}
