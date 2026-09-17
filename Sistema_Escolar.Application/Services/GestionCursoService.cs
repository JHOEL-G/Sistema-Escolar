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
    public class GestionCursoService : IGestionCursoService
    {
        private readonly IGestionCursoRepository _repo;
        private readonly INotificacionService _service;
        private readonly IFileStorageService _fileService;

        public GestionCursoService(IGestionCursoRepository repo, INotificacionService service, IFileStorageService fileService)
        {
            _repo = repo;
            _service = service;
            _fileService = fileService;
        }

        public async Task<OperationResult> CrearGestionCurso(CrearGestionCursoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NombreCurso))
            {
                return OperationResult.Fail("El nombre del curso es requerido");
            }

            if (dto.CursoId <= 0)
            {
                return OperationResult.Fail("El ID del curso es inválido");
            }

            if (dto.Temas == null || !dto.Temas.Any())
            {
                return OperationResult.Fail("Debe seleccionar al menos un tema");
            }

            if (dto.InscripcionAutomatica && (dto.Criterios == null || !dto.Criterios.Any()))
            {
                return OperationResult.Fail("Debe definir al menos un criterio para inscripción automática");
            }

            var resultado = await _repo.CreateGestionCurso(dto);

            if (resultado.Success)
            {
                var gestionCursoId = Convert.ToInt32(resultado.Data);

                var usuariosANotificar = await _service.ObtenerUsuariosParaNotificar(gestionCursoId);

                foreach (var u in usuariosANotificar)
                {
                    var mensaje = u.Tipo switch
                    {
                        "NOT_ASIGNACIONES" => $"Fuiste asignado como evaluador del curso \"{dto.NombreCurso}\".",
                        "NOT_CAPACITACION_MASTER" => $"Fuiste inscrito al curso \"{dto.NombreCurso}\".",
                        _ => $"Hay un nuevo curso disponible: \"{dto.NombreCurso}\"."
                    };

                    await _service.CrearNotificacion(new CrearNotificacionDTO
                    {
                        UsuarioId = u.UsuarioId,
                        Tipo = u.Tipo,
                        Mensaje = mensaje,
                        ReferenciaId = gestionCursoId
                    });
                }
            }

            return resultado;
        }

        public async Task<OperationResult<IEnumerable<GestionCursoBaseDTO>>> GetIdGestiomCurso(int id)
        {
            var resultado = await _repo.GetIdGestiomCurso(id);

            return resultado;
        }

        public async Task<OperationResult> ActualizarGestionCurso(CrearGestionCursoDTO dto)
        {
            var resultado = await _repo.UpdateGestionCurso(dto);

            if (resultado.Success)
            {
                var gestionCursoId = dto.GestionCursoId ?? 0;

                var usuariosANotificar = await _service.ObtenerUsuariosParaNotificar(gestionCursoId);

                foreach (var u in usuariosANotificar)
                {
                    var mensaje = u.Tipo switch
                    {
                        "NOT_ASIGNACIONES" => $"Fuiste reasignado como evaluador del curso \"{dto.NombreCurso}\".",
                        "NOT_CAPACITACION_MASTER" => $"El curso \"{dto.NombreCurso}\" al que estás inscrito fue actualizado.",
                        _ => $"El curso \"{dto.NombreCurso}\" ha sido actualizado."
                    };

                    await _service.CrearNotificacion(new CrearNotificacionDTO
                    {
                        UsuarioId = u.UsuarioId,
                        Tipo = u.Tipo,
                        Mensaje = mensaje,
                        ReferenciaId = gestionCursoId
                    });
                }
            }

            return resultado;
        }

        public async Task<OperationResult<IEnumerable<GestionCursoResponseDTO>>> ListarGestionCursos()
        {
            var resultado = await _repo.GetAllGestionCursos();

            if (!resultado.Success || resultado.Data is null)
                return OperationResult<IEnumerable<GestionCursoResponseDTO>>.Fail("Error al obtener los cursos");

            var cursos = resultado.Data.ToList();

            foreach (var c in cursos)
            {
                if (!string.IsNullOrEmpty(c.ImagenPortadaPath))
                    c.ImagenPortadaPath = _fileService.GetPresignedUrl(c.ImagenPortadaPath, 60);
            }

            return OperationResult<IEnumerable<GestionCursoResponseDTO>>.Ok(cursos, "Cursos obtenidos correctamente");
        }
    }
}
