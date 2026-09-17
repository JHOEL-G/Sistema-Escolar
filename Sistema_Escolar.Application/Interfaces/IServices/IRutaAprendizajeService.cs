using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IRutaAprendizajeService
    {
        Task<OperationResult<IEnumerable<ListarRutaAprendizajeDTO>>> ObtenerRutaAaprendizaje();

        Task<OperationResult> CrearRutaAprendizaje(RutaAprendizajeDTO dto, Stream? imagenStream = null, string? nombreImagen = null);

        Task<OperationResult<IEnumerable<ParticipanteRutaDTO>>> ObtenerParticipantesRuta(int rutaId);

        Task<OperationResult<RutaUsuarioConProgresoDTO>> ObtenerRutasPorUsuario(int usuarioId);

        Task<OperationResult<RutaAprendizajeDetalleDTO>> ObtenerRutaAprendizajePorId(int rutaId);

        Task<OperationResult> ActualizarRutaAprendizaje(int rutaId, RutaAprendizajeDTO dto, Stream? imagenStream = null, string? nombreImagen = null);
    }
}
