using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IUsuarioCursoRepository
    {
        Task<OperationResult> CreateUsuarioCurso(UsuarioCursoDTO dto);

        Task<IEnumerable<UsuarioCursoDTO?>> GetPorIdUsuarioCusrso(string id);

        Task<IEnumerable<ParticipantesDTO>> GetParticipantes(int cursoId);

        Task<OperationResult> RegistrarProgresoRecurso(int usuarioId, int cursoId, int moduloRecursoId);

        Task<OperationResult> CalificarRecurso(CalificacionRecursoDTO dto);
        Task<IEnumerable<CalificacionResumenDTO>> ObtenerCalificaciones(int cursoId);
        Task<IEnumerable<ProgresoRecursoDTO>> ObtenerProgreso(int usuarioId, int cursoId);
        Task<IEnumerable<ParticipanteConRecursosDTO>> GetParticipantesConRecursos(int cursoId);
        Task<IEnumerable<RespuestaAlumnoDTO>> ObtenerRespuestasAlumno(int evaluacionId, int usuarioId);
        Task<OperationResult> GuardarRespuestasEvaluacion(GuardarRespuestasDTO dto);
        Task<bool> VerificarInscripcion(int usuarioId, int cursoId);
    }
}
