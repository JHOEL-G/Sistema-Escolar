using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class GestionDocumentoService : IGestionDocumentoService
    {
        private readonly IGestionDocumentoRepository _repo;
        private readonly IFileStorageService _service;

        public GestionDocumentoService(IGestionDocumentoRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> ActualizarCarpeta(CarpetaActualizarDTO dto)
        {
            if (dto.ExploradorId <= 0) return OperationResult.Fail("El id no es valido");

            return await _repo.UpdateCarpeta(dto);
        }

        public async Task<OperationResult> CrearCarpeta(CarpetaInsertarDTO dto)
        {
            if (dto.NombreCarpeta == null) return OperationResult.Fail("Es necesario un nombre para la carpeta");

            var resultado = await _repo.CreateCarpeta(dto);

            return resultado;
        }

        public async Task<OperationResult> CrearDocumento(DocumentoInsertarDTO dto, Stream? documentoStream = null, string? nombreDocumento = null)
        {
            if (dto.TituloDocumento == null) return OperationResult.Fail("Es necesario el titulo del documento");

            if (documentoStream != null && !string.IsNullOrEmpty(nombreDocumento))
            {
                dto.DocumentoPath = await _service.UploadFile(documentoStream, nombreDocumento, "arbol-documento");
            }

            var resultado = await _repo.CreateDocumento(dto);

            return resultado;
        }

        public async Task<OperationResult> EliminarDocumento(int gestionId)
        {
            if (gestionId <= 0) return OperationResult.Fail("El id no existe");

            var resultado = await _repo.DeleteDocumento(gestionId);

            return resultado;
        }

        public async Task<OperationResult> EliminarCarpeta(int exploradorId)
        {
            if (exploradorId <= 0) return OperationResult.Fail("El id no existe");

            var resultado = await _repo.DeleteCarpeta(exploradorId);

            return resultado;
        }

        public async Task<OperationResult<List<DocumentoResponseDTO>>> ListarDocumentosPorCarpeta(int exploradorId)
        {
            if (exploradorId <= 0) return OperationResult<List<DocumentoResponseDTO>>.Fail("ID de explorador inválido");

            var resultado = await _repo.GetDocumentosPorCarpeta(exploradorId);

            if (!resultado.Success || resultado.Data == null)
            {
                return resultado; 
            }

            var documentos = resultado.Data;

            foreach (var doc in documentos)
            {
                if (!string.IsNullOrEmpty(doc.DocumentoPath)) doc.DocumentoPath = _service.GetPresignedUrl(doc.DocumentoPath, 60);
            }

            return OperationResult<List<DocumentoResponseDTO>>.Ok(documentos, "Documentos obtenidos correctamente");
        }

        public async Task<OperationResult<List<CarpetaResponseDTO>>> ListarArbolCarpetas()
        {
            var resultado = await _repo.GetArbolCarpetas();

            if (resultado == null || !resultado.Success)
            {
                return OperationResult<List<CarpetaResponseDTO>>.Fail("No se pudo encontrar los datos o hubo un error en el servidor");
            }

            return resultado;
        }

        public async Task<OperationResult> ActualizarDocumento(DocumentoActualizarDTO dto)
        {
            if (dto.GestionId <= 0) return OperationResult.Fail("EL id no existe");

            var resultado = await _repo.UpdateDocumento(dto);

            return resultado;
        }
    }
}
