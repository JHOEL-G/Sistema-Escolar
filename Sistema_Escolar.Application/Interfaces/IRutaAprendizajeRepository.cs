using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRutaAprendizajeRepository
    {
        Task<OperationResult> CreateRutaAprendizaje(RutaAprendizajeDTO dto);

        Task<IEnumerable<ListarRutaAprendizajeDTO>> GetRutaAprendizaje();
        Task<IEnumerable<ParticipanteRutaDTO>> GetParticipantesRuta(int rutaId);
        Task<(IEnumerable<RutaUsuarioDTO> Rutas, IEnumerable<CursoProgresoDTO> Cursos)> GetRutasPorUsuario(int usuarioId);
        Task<RutaAprendizajeDetalleDTO?> GetRutaById(int rutaId);
        Task<OperationResult> UpdateRutaAprendizaje(int rutaId, RutaAprendizajeDTO dto);
    }
}
