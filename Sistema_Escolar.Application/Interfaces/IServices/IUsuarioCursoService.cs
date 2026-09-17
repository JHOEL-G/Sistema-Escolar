using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IUsuarioCursoService
    {
        Task<OperationResult> CrearInscripcionCurso(UsuarioCursoDTO dto);

        Task<OperationResult<IEnumerable<UsuarioCursoDTO?>>> ObtenerCursoUsuarioId(string id);

        Task<OperationResult<IEnumerable<ParticipantesDTO>>> ObtenerParticipantes(int curentId);

        Task<OperationResult> RegistrarProgresoRecurso(ProgresoDTO dto);

        Task<OperationResult> CalificarRecurso(CalificacionRecursoDTO dto);

        Task<OperationResult> ObtenerCalificaciones(int cursoId);
        Task<IEnumerable<ProgresoRecursoDTO>> ObtenerProgreso(int usuarioId, int cursoId);
        Task<OperationResult> ObtenerParticipantesConRecursos(int cursoId);
        Task<OperationResult<IEnumerable<RespuestaAlumnoDTO>>> ObtenerRespuestasAlumno(int evaluacionId, int usuarioId);
        Task<OperationResult> GuardarRespuestasEvaluacion(GuardarRespuestasDTO dto);
        Task<bool> VerificarInscripcion(int usuarioId, int cursoId);
    }
}
