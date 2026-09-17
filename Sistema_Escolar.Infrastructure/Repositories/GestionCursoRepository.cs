using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class GestionCursoRepository : IGestionCursoRepository
    {
        private readonly ConfiaContext _context;
        private readonly JsonSerializerOptions _jsonOptions;

        public GestionCursoRepository(ConfiaContext context, JsonSerializerOptions jsonOptions)
        {
            _context = context;
            _jsonOptions = jsonOptions;
        }

        public async Task<OperationResult> CreateGestionCurso(CrearGestionCursoDTO dto)
        {
            try
            {
                var jsonOptions = new JsonSerializerOptions       
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var parametros = new[]
                {
                    new SqlParameter("@CursoId", dto.CursoId),
                    new SqlParameter("@NombreCurso", dto.NombreCurso),
                    new SqlParameter("@Privacidad", dto.Privacidad ?? "Privado"),
                    new SqlParameter("@InscripcionAutomatica", dto.InscripcionAutomatica),
                    new SqlParameter("@PermitirDesinscripcion", dto.PermitirDesinscripcion),
                    new SqlParameter("@Gamificacion", dto.Gamificacion),
                    new SqlParameter("@CreadoPor", dto.CreadoPor),
                    new SqlParameter("@TemasJSON", JsonSerializer.Serialize(dto.Temas, jsonOptions)),          
                    new SqlParameter("@ParticipantesJSON",
                        dto.Participantes?.Any() == true
                            ? JsonSerializer.Serialize(dto.Participantes, jsonOptions)                          
                            : (object)DBNull.Value),
                    new SqlParameter("@EvaluadoresJSON",
                        dto.Evaluadores?.Any() == true
                            ? JsonSerializer.Serialize(dto.Evaluadores, jsonOptions)                             
                            : (object)DBNull.Value),
                    new SqlParameter("@CriteriosJSON",
                        dto.Criterios?.Any() == true
                            ? JsonSerializer.Serialize(dto.Criterios, jsonOptions)                               
                            : (object)DBNull.Value),
                                new SqlParameter("@VisibilidadJSON", dto.Visibilidad?.Any() == true
                ? JsonSerializer.Serialize(dto.Visibilidad, jsonOptions) : (object)DBNull.Value),
                };

                var resultado = await _context.Database
                    .SqlQueryRaw<StoreProcedureResult>(
                        "EXEC sp_CrearGestionCurso @CursoId, @NombreCurso, @Privacidad, " +
                        "@InscripcionAutomatica, @PermitirDesinscripcion, @Gamificacion, " +
                        "@CreadoPor, @TemasJSON, @ParticipantesJSON, @EvaluadoresJSON, @CriteriosJSON, @VisibilidadJSON",
                        parametros)
                    .ToListAsync();

                var sp = resultado.FirstOrDefault();

                if (sp == null || sp.ResultId <= 0)
                {
                    return OperationResult.Fail(sp?.Mensaje ?? "Error al crear la gestión del curso"); 
                }

                return OperationResult.Ok(sp.Mensaje);
            }
            catch (SqlException ex)
            {
                return OperationResult.Fail($"Error en base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<OperationResult<IEnumerable<GestionCursoResponseDTO>>> GetAllGestionCursos()
        {
            var resultado = await _context.Database.SqlQueryRaw<GestionCursoResponseDTO>("EXEC sp_ListarGestionesCursos").ToListAsync();

            foreach (var item in resultado)
                DeserializarCamposJson(item);

            return OperationResult<IEnumerable<GestionCursoResponseDTO>>.Ok(resultado);
        }

        public async Task<OperationResult<IEnumerable<GestionCursoBaseDTO>>> GetIdGestiomCurso(int id)
        {
            if (id <= 0)
                return OperationResult<IEnumerable<GestionCursoBaseDTO>>.Fail("El id no es válido");

            var resultado = await _context.Database.SqlQueryRaw<GestionCursoResponseDTO>(
                "EXEC sp_ObtenerGestionCursoPorId @GestionCursoId",
                new SqlParameter("@GestionCursoId", id)).ToListAsync();

            foreach (var item in resultado)
                DeserializarCamposJson(item);

            return OperationResult<IEnumerable<GestionCursoBaseDTO>>.Ok(resultado);
        }

        public async Task<OperationResult> UpdateGestionCurso(CrearGestionCursoDTO dto)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@GestionCursoId", dto.GestionCursoId),
                new SqlParameter("@NombreCurso", dto.NombreCurso),
                new SqlParameter("@Privacidad", dto.Privacidad ?? "Privado"),
                new SqlParameter("@InscripcionAutomatica", dto.InscripcionAutomatica),
                new SqlParameter("@PermitirDesinscripcion", dto.PermitirDesinscripcion),
                new SqlParameter("@Gamificacion", dto.Gamificacion),
                new SqlParameter("@TemasJSON", JsonSerializer.Serialize(dto.Temas, _jsonOptions)),
                new SqlParameter("@ParticipantesJSON", dto.Participantes?.Any() == true
                    ? JsonSerializer.Serialize(dto.Participantes, _jsonOptions) : (object)DBNull.Value),
                new SqlParameter("@EvaluadoresJSON", dto.Evaluadores?.Any() == true
                    ? JsonSerializer.Serialize(dto.Evaluadores, _jsonOptions) : (object)DBNull.Value),
                new SqlParameter("@CriteriosJSON", dto.Criterios?.Any() == true
                    ? JsonSerializer.Serialize(dto.Criterios, _jsonOptions) : (object)DBNull.Value),
                new SqlParameter("@VisibilidadJSON", dto.Visibilidad?.Any() == true
                ? JsonSerializer.Serialize(dto.Visibilidad, _jsonOptions) : (object)DBNull.Value),
            };

                var resultado = await _context.Database
                    .SqlQueryRaw<StoreProcedureResult>(
                        "EXEC sp_ModificarGestionCurso @GestionCursoId, @NombreCurso, @Privacidad, " +
                        "@InscripcionAutomatica, @PermitirDesinscripcion, @Gamificacion, " +
                        "@TemasJSON, @ParticipantesJSON, @EvaluadoresJSON, @CriteriosJSON, @VisibilidadJSON",
                        parametros)
                    .ToListAsync();

                var sp = resultado.FirstOrDefault();

                if (sp == null || sp.ResultId <= 0)
                    return OperationResult.Fail(sp?.Mensaje ?? "Error al actualizar");

                return OperationResult.Ok(sp.Mensaje);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Error inesperado: {ex.Message}");
            }
        }

        private void DeserializarCamposJson(GestionCursoResponseDTO item)
        {
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!string.IsNullOrEmpty(item.TemasJSON))
                item.Temas = JsonSerializer.Deserialize<List<TemaSeleccionadoDTO>>(item.TemasJSON, opts) ?? [];

            if (!string.IsNullOrEmpty(item.ParticipantesJSON))
                item.Participantes = JsonSerializer
                    .Deserialize<List<JsonElement>>(item.ParticipantesJSON, opts)!
                    .Select(e => e.GetProperty("UsuarioId").GetInt32())
                    .ToList();

            if (!string.IsNullOrEmpty(item.EvaluadoresJSON))
                item.Evaluadores = JsonSerializer
                    .Deserialize<List<JsonElement>>(item.EvaluadoresJSON, opts)!
                    .Select(e => e.GetProperty("UsuarioId").GetInt32())
                    .ToList();

            if (!string.IsNullOrEmpty(item.CriteriosJSON))
                item.Criterios = JsonSerializer.Deserialize<List<CriterioInscripcionDTO>>(item.CriteriosJSON, opts) ?? [];

            if (!string.IsNullOrEmpty(item.VisibilidadJSON))
                item.Visibilidad = JsonSerializer.Deserialize<List<VisibilidadGrupoDTO>>(item.VisibilidadJSON, opts) ?? [];
        }
    }
}
