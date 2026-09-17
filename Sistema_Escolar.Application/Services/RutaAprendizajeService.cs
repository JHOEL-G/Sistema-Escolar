using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class RutaAprendizajeService : IRutaAprendizajeService
    {
        private readonly IRutaAprendizajeRepository _repo;
        private readonly IFileStorageService _service;


        public RutaAprendizajeService(IRutaAprendizajeRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> ActualizarRutaAprendizaje(int rutaId, RutaAprendizajeDTO dto, Stream? imagenStream = null, string? nombreImagen = null)
        {
            if (rutaId <= 0)
                return OperationResult.Fail("El ID del curso no es válido para edición.");

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
            {
                dto.ImagenPortada = await _service.UploadFile(imagenStream, nombreImagen, "portadas_imagen");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var resultado = await _repo.UpdateRutaAprendizaje(rutaId, dto);
                    if (!resultado.Success) return resultado;

                    scope.Complete();
                    return OperationResult.Ok(resultado, "Ruta de Aprendizaje actualizado exitosamente.");
                }
                catch (Exception ex)
                {
                    return OperationResult.Fail("Error al editar la ruta de aprendizaje completo: " + ex.Message);
                }
            }
        }

        public async Task<OperationResult> CrearRutaAprendizaje(RutaAprendizajeDTO dto, Stream? imagenStream = null, string? nombreImagen = null)
        {
            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
            {
                dto.ImagenPortada = await _service.UploadFile(imagenStream, nombreImagen, "portada-imagen");
            }

            var resultado = await _repo.CreateRutaAprendizaje(dto);

            return resultado;
        }

        public async Task<OperationResult<IEnumerable<ParticipanteRutaDTO>>> ObtenerParticipantesRuta(int rutaId)
        {
            if (rutaId <= 0)
                return OperationResult<IEnumerable<ParticipanteRutaDTO>>.Fail("El ID de la ruta es inválido.");

            var resultado = await _repo.GetParticipantesRuta(rutaId);

            return OperationResult<IEnumerable<ParticipanteRutaDTO>>.Ok(resultado ?? new List<ParticipanteRutaDTO>());
        }

        public async Task<OperationResult<IEnumerable<ListarRutaAprendizajeDTO>>> ObtenerRutaAaprendizaje()
        {
            var resultado = await _repo.GetRutaAprendizaje();

            resultado ??= new List<ListarRutaAprendizajeDTO>();

            foreach (var r in resultado)
            {
                if (!string.IsNullOrEmpty(r.ImagenPortada)) r.ImagenPortada = _service.GetPresignedUrl(r.ImagenPortada, 60);
            }

            if (resultado != null && resultado.Any())
            {
                return OperationResult<IEnumerable<ListarRutaAprendizajeDTO>>.Ok(resultado);
            }

            return OperationResult<IEnumerable<ListarRutaAprendizajeDTO>>.Ok(new List<ListarRutaAprendizajeDTO>());
        }

        public async Task<OperationResult<RutaAprendizajeDetalleDTO>> ObtenerRutaAprendizajePorId(int rutaId)
        {
            if (rutaId <= 0)
                return OperationResult<RutaAprendizajeDetalleDTO>.Fail("ID de ruta inválido.");

            var resultado = await _repo.GetRutaById(rutaId);

            if (resultado == null)
                return OperationResult<RutaAprendizajeDetalleDTO>.Fail("Ruta no encontrada.");

            if (!string.IsNullOrEmpty(resultado.ImagenPortada))
                resultado.ImagenPortada = _service.GetPresignedUrl(resultado.ImagenPortada, 60);

            return OperationResult<RutaAprendizajeDetalleDTO>.Ok(resultado);
        }

        public async Task<OperationResult<RutaUsuarioConProgresoDTO>> ObtenerRutasPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                return OperationResult<RutaUsuarioConProgresoDTO>.Fail("ID de usuario inválido.");

            var (rutas, cursosProgreso) = await _repo.GetRutasPorUsuario(usuarioId);

            var rutasList = rutas?.ToList() ?? new List<RutaUsuarioDTO>();

            foreach (var r in rutasList)
            {
                if (!string.IsNullOrEmpty(r.ImagenPortada))
                    r.ImagenPortada = _service.GetPresignedUrl(r.ImagenPortada, 60);
            }

            return OperationResult<RutaUsuarioConProgresoDTO>.Ok(new RutaUsuarioConProgresoDTO
            {
                Rutas = rutasList,
                CursosProgreso = cursosProgreso ?? new List<CursoProgresoDTO>()
            });

        }
    }
}
