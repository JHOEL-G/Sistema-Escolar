using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<ListarCursoDTO>> GetCursos(bool soloActivos = true);
        Task<CursoDTO?> GetCursoById(int id);
        Task<OperationResult> CreateCurso(CursoDTO cursoDto);
        Task<OperationResult> UpdateCurso(CursoDTO cursoDto);

        Task<int> ObtenerIdRelacion(int cursoId, int moduloOrden, int recursoOrden);

        Task<OperationResult> DuplicarCurso(int cursoId);

        Task<List<RecursoIdCreadoDTO>> AgregarRelacionesModuloRecurso(int cursoId, List<ModuloDTO> modulos, List<RecursoDTO> recursos);

        Task ActualizarPonderacionRecursos(List<PonderacionItemDTO> ponderaciones);

        Task ActualizarCriterioCurso(int cursoId, decimal? calificacion, int? requisitoAvance);

        Task<OperationResult> CambiarEstadoCurso(int cursoId, int adminId, string accion, string? motivo = null);

        Task<OperationResult> EliminarCurso(int cursoId, int adminId, string? motivo = null);
    }
}
