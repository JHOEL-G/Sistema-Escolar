using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class TemaSerice : ITemaService
    {
        private readonly ITemaRepository _repo;
        private readonly IFileStorageService _service;

        public TemaSerice(ITemaRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> ActualizarTema(TtemaDTO temaDTO, Stream? imagenStream = null, string? nombreImagen = null)
        {
            if (temaDTO.TemaId <= 0) return OperationResult.Fail("ID de tema no válido.");

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
            {
                temaDTO.ImagenPortada = await _service.UploadFile(imagenStream, nombreImagen, "temaW-imagen");
            }

            return await _repo.UpdateTema(temaDTO);
        }

        public async Task<OperationResult> CrearTema(TtemaDTO temaDTO, Stream? imagenStream = null, string? nombreImagen = null)
        {
            if (string.IsNullOrWhiteSpace(temaDTO.NombreTema)) return OperationResult.Fail("El nombre del tema no puede estar vacio");

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen)) temaDTO.ImagenPortada = await _service.UploadFile(imagenStream, nombreImagen, "tema-imagen");
            else temaDTO.ImagenPortada = null;

            var resultado = await _repo.CreateTema(temaDTO);

            return resultado;
        }

        public async Task<OperationResult<IEnumerable<TtemaDTO?>>> ObtenerTemaId(int id)
        {
            var resultado = await _repo.GetTtemaId(id);

            if (resultado == null || !resultado.Any())
                return OperationResult<IEnumerable<TtemaDTO?>>.Fail("No se encontró el tema solicitado.");

            foreach (var tema in resultado)
            {
                if (tema != null && !string.IsNullOrEmpty(tema.ImagenPortada))
                {
                    tema.ImagenPortada = _service.GetPresignedUrl(tema.ImagenPortada, 60);
                }
            }

            return OperationResult<IEnumerable<TtemaDTO?>>.Ok(resultado, "Datos obtenidos correctamente");
        }

        public async Task<OperationResult<IEnumerable<TtemaDTO>>> ObtenerTemas()
        {
            var temas = await _repo.GetTtemas();

            temas ??= new List<TtemaDTO>();

            foreach (var tema in temas)
            {
                if (!string.IsNullOrEmpty(tema.ImagenPortada)) tema.ImagenPortada = _service.GetPresignedUrl(tema.ImagenPortada, 60);
            }

            return OperationResult<IEnumerable<TtemaDTO>>.Ok(temas, "Temas obtenidos correctamente");
        }
    }
}
