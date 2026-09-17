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
    public class UsuarioCursoService : IUsuarioCursoService
    {
        private readonly IUsuarioCursoRepository _repo;
        private readonly IFileStorageService _service;

        public UsuarioCursoService (IUsuarioCursoRepository repo, IFileStorageService serivce)
        {
            _repo = repo;
            _service = serivce;
        }

        public async Task<OperationResult> CalificarRecurso(CalificacionRecursoDTO dto)
        {
            var resultado = await _repo.CalificarRecurso(dto);

            if (resultado == null) return OperationResult.Fail("No se pudo calificar el recurso.");

            return OperationResult.Ok(resultado, "Recurso calificado exitosamente.");
        }

        public async Task<OperationResult> CrearInscripcionCurso(UsuarioCursoDTO dto)
        {
            var resultado = await _repo.CreateUsuarioCurso(dto);

            return resultado;
        }

        public async Task<OperationResult> ObtenerCalificaciones(int cursoId)
        {
            var resultado = await _repo.ObtenerCalificaciones(cursoId);
            return OperationResult.Ok(resultado, "Calificaciones obtenidas.");
        }

        public async Task<OperationResult<IEnumerable<UsuarioCursoDTO?>>> ObtenerCursoUsuarioId(string id)
        {
            var resultado = await _repo.GetPorIdUsuarioCusrso(id);

            if (resultado == null || !resultado.Any())
            {
                return OperationResult<IEnumerable<UsuarioCursoDTO?>>.Fail("El usuario no tiene cursos inscritos.");
            }

            foreach (var curso in resultado)
            {
                if (!string.IsNullOrEmpty(curso.ImagenPortadaPath))
                {
                    curso.ImagenPortadaPath = _service.GetPresignedUrl(curso.ImagenPortadaPath, 60);
                }
            }

            return OperationResult<IEnumerable<UsuarioCursoDTO?>>.Ok(resultado);
        }

        public async Task<OperationResult<IEnumerable<ParticipantesDTO>>> ObtenerParticipantes(int curentId)
        {
            var resultado = await _repo.GetParticipantes(curentId);

            return OperationResult<IEnumerable<ParticipantesDTO>>.Ok(resultado);
        }

        public async Task<IEnumerable<ProgresoRecursoDTO>> ObtenerProgreso(int usuarioId, int cursoId)
        {
            return await _repo.ObtenerProgreso(usuarioId, cursoId);
        }

        public async Task<OperationResult> RegistrarProgresoRecurso(ProgresoDTO dto)
        {
            var resultado = await _repo.RegistrarProgresoRecurso(dto.UsuarioId, dto.CursoId, dto.ModuloRecursoId);

            return resultado;
        }

        public async Task<OperationResult> ObtenerParticipantesConRecursos(int cursoId)
        {
            var resultado = await _repo.GetParticipantesConRecursos(cursoId);
            return OperationResult.Ok(resultado, "Participantes con recursos obtenidos.");
        }

        public async Task<OperationResult<IEnumerable<RespuestaAlumnoDTO>>> ObtenerRespuestasAlumno(int evaluacionId, int usuarioId)
        {
            var resultado = await _repo.ObtenerRespuestasAlumno(evaluacionId, usuarioId);

            return OperationResult<IEnumerable<RespuestaAlumnoDTO>>.Ok(resultado, "Respuestas de evaluacion del alumno obtenidas.");
        }

        public async Task<OperationResult> GuardarRespuestasEvaluacion(GuardarRespuestasDTO dto)
        {
            var resultado = await _repo.GuardarRespuestasEvaluacion(dto);

            return resultado;
        }

        public async Task<bool> VerificarInscripcion(int usuarioId, int cursoId)
        {
            var resultado = await _repo.VerificarInscripcion(usuarioId, cursoId);

            return resultado;
        }
    }
}
