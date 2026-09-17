using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface ICursoService
    {
        Task<OperationResult> CrearCurso(CursoDTO cursoDTO, Stream? imagenStream = null, string? nombreImagen = null, Stream? videoStream = null, string? nombreVideo = null, List<IFormFile>? archivosRecursos = null);
        Task<OperationResult> EditarCurso(CursoDTO cursoDTO, Stream? imagenStream = null, string? nombreImagen = null, Stream? videoStream = null, string? nombreVideo = null, List<IFormFile>? archivosRecursos = null);
        Task<OperationResult<IEnumerable<ListarCursoDTO>>> ObtenerCursos(bool soloActivos = true);
        Task<OperationResult<CursoDTO?>> ObtenerCursoId(int id);
        Task<OperationResult> DuplicarCurso(int cursoId);
        Task<OperationResult> AgregarRecursosCurso(int cursoId, List<ModuloDTO> modulos, List<RecursoDTO> recursos, List<IFormFile>? archivosRecursos = null);
        Task<OperationResult> ActualizarPonderacion(int cursoId, List<PonderacionItemDTO> ponderaciones, decimal? calificacion = null, int? requisitoAvance = null);

        Task<OperationResult> CambiarEstadoCurso(int cursoId, int adminId, string accion, string? motivo = null);
        Task<OperationResult> EliminarCurso(int cursoId, int adminId, string? motivo = null);
    }
}
