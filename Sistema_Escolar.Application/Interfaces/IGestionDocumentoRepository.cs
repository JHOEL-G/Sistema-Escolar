using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IGestionDocumentoRepository
    {
        Task<OperationResult> CreateCarpeta(CarpetaInsertarDTO dto);
        Task<OperationResult> UpdateCarpeta(CarpetaActualizarDTO dto);
        Task<OperationResult> DeleteCarpeta(int exploradorId);
        Task<OperationResult<List<CarpetaResponseDTO>>> GetArbolCarpetas();


        Task<OperationResult> CreateDocumento(DocumentoInsertarDTO dto);
        Task<OperationResult> UpdateDocumento(DocumentoActualizarDTO dto);
        Task<OperationResult> DeleteDocumento(int gestionId);
        Task<OperationResult<List<DocumentoResponseDTO>>> GetDocumentosPorCarpeta(int exploradorId);

    }
}
