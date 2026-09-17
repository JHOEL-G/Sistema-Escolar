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
    public class RegistroPrevioService : IRegistroPrevioService
    {
        private readonly IRegistroPrevioRepository _repo;
        private readonly IFileStorageService _service;

        public RegistroPrevioService(IRegistroPrevioRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearRegistroPrevio(RegistroPrevioDTO registroPrevioDto, Stream? imagenStream = null, string? nombreImagen = null)
        {
            if (string.IsNullOrWhiteSpace(registroPrevioDto.NombreCurso)) return OperationResult.Fail("El nombre del curso es obligatorio.");

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen)) registroPrevioDto.ImagenPath = await _service.UploadFile(imagenStream, nombreImagen, "registros-previos");
            else registroPrevioDto.ImagenPath = null;
            
            var resultado = await _repo.CreateRegistroPrevio(registroPrevioDto);

            return resultado;
        }

        public async Task<OperationResult> EditarRegistroPrevio(RegistroPrevioDTO registroPrevioDto)
        {
            if (registroPrevioDto.PrevioId <= 0) return OperationResult.Fail("El ID del registro previo es inválido.");

            return await _repo.UpdateRegistroPrevio(registroPrevioDto);
        }

        public async Task<OperationResult<RegistroPrevioDTO?>> ObtenerRegistroPrevioPorId(int id)
        {
            var previo = await _repo.GetRegistroPrevioById(id);

            if (previo == null) return OperationResult<RegistroPrevioDTO?>.Fail("Registro previo no encontrado.");

            if (!string.IsNullOrEmpty(previo.ImagenPath)) previo.ImagenPath = _service.GetPresignedUrl(previo.ImagenPath, 60);

            return OperationResult<RegistroPrevioDTO?>.Ok(previo, "Curso obtenido correctamente");
        }

        public async Task<OperationResult<IEnumerable<RegistroPrevioDTO>>> ObtenerRegistrosPrevios()
        {
            var previos = await _repo.GetRegistroPrevios();

            previos ??= new List<RegistroPrevioDTO>();

            foreach (var previo in previos)
            {
                if (!string.IsNullOrEmpty(previo.ImagenPath)) previo.ImagenPath = _service.GetPresignedUrl(previo.ImagenPath, 60);
            }

            return OperationResult<IEnumerable<RegistroPrevioDTO>>.Ok(previos, "Registros previos obtenidos correctamente");
        }
    }
}
