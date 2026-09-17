using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using Sistema_Escolar_Confia.Models;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class UsuarioCursoRepository : IUsuarioCursoRepository
    {
        private readonly ConfiaContext _context;

        public UsuarioCursoRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CalificarRecurso(CalificacionRecursoDTO dto)
        {
            Console.WriteLine($"TipoRecurso={dto.TipoRecurso}, RecursoId={dto.RecursoId}, UsuarioId={dto.UsuarioId}, CursoId={dto.CursoId}, Calificacion={dto.Calificacion}");

            var parametros = new[]
            {
                new SqlParameter("@TipoRecurso",     dto.TipoRecurso),
                new SqlParameter("@RecursoId",       dto.RecursoId),
                new SqlParameter("@UsuarioId",       dto.UsuarioId),
                new SqlParameter("@CursoId",         dto.CursoId),
                new SqlParameter("@Calificacion",    dto.Calificacion),
                new SqlParameter("@Comentario",      (object?)dto.Comentario ?? DBNull.Value),
                new SqlParameter("@CalificadoPorId", (object?)dto.CalificadoPorId ?? DBNull.Value)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_CalificarRecurso @TipoRecurso, @RecursoId, @UsuarioId, @CursoId, @Calificacion, @Comentario, @CalificadoPorId",
                parametros
            ).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Error al calificar");
            return sp.ResultId > 0 ? OperationResult.Ok(sp.Mensaje) : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> CreateUsuarioCurso(UsuarioCursoDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", dto.UsuarioId),
                new SqlParameter("@CursoId", dto.CursoId)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_InscribirUsuarioCurso @UsuarioId, @CursoId", parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al crear la inscipcion");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<IEnumerable<ParticipantesDTO>> GetParticipantes(int cursoId)
        {
            var resultado = await _context.Database.SqlQueryRaw<ParticipantesDTO>(
            "EXEC sp_ObtenerParticipantesPorCurso @CursoId",
            new SqlParameter("@CursoId", cursoId)).ToListAsync();

            return resultado ?? new List<ParticipantesDTO>();
        }

        public async Task<IEnumerable<UsuarioCursoDTO?>> GetPorIdUsuarioCusrso(string keycloakId)
        {
            if (!Guid.TryParse(keycloakId, out Guid guidKeycloak))
            {
                return new List<UsuarioCursoDTO>();
            }

            var resultado = await _context.Database.SqlQueryRaw<UsuarioCursoDTO>(
                "EXEC sp_ObtenerCursosPorUsuarioGuid @KeycloakId",
                new SqlParameter("@KeycloakId", SqlDbType.UniqueIdentifier) { Value = guidKeycloak }
            ).ToListAsync();

            return resultado ?? new List<UsuarioCursoDTO>();
        }

        public async Task<IEnumerable<CalificacionResumenDTO>> ObtenerCalificaciones(int cursoId)
        {
            var resultado = await _context.Database.SqlQueryRaw<CalificacionResumenDTO>(
                "EXEC sp_ObtenerCalificacionesPorCurso @CursoId",
                new SqlParameter("@CursoId", cursoId)
            ).ToListAsync();

            return resultado ?? new List<CalificacionResumenDTO>();
        }

        public async Task<IEnumerable<ProgresoRecursoDTO>> ObtenerProgreso(int usuarioId, int cursoId)
        {
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", usuarioId),
                new SqlParameter("@CursoId", cursoId)
            };

            var resultado = await _context.Database.SqlQueryRaw<ProgresoRecursoDTO>(
                "EXEC sp_ObtenerProgresoUsuarioCurso @UsuarioId, @CursoId", parametros
            ).ToListAsync();

            return resultado ?? new List<ProgresoRecursoDTO>();
        }

        public async Task<OperationResult> RegistrarProgresoRecurso(int usuarioId, int cursoId, int moduloRecursoId)
        {
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", usuarioId),
                new SqlParameter("@CursoId", cursoId),
                new SqlParameter("@ModuloRecursoId", moduloRecursoId)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                 "EXEC sp_RegistrarProgresoRecurso @UsuarioId, @CursoId, @ModuloRecursoId", parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Error al registrar progreso");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<IEnumerable<ParticipanteConRecursosDTO>> GetParticipantesConRecursos(int cursoId)
        {
            var connectionString = _context.Database.GetConnectionString();
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(
                "EXEC dbo.sp_ObtenerParticipantesConRecursos @CursoId", conn);
            cmd.Parameters.AddWithValue("@CursoId", cursoId);

            using var reader = await cmd.ExecuteReaderAsync();

            var participantes = new List<ParticipanteConRecursosDTO>();
            while (await reader.ReadAsync())
            {
                participantes.Add(new ParticipanteConRecursosDTO
                {
                    InscripcionId = reader.GetInt32(reader.GetOrdinal("InscripcionId")),
                    UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    ApeLLido = reader.GetString(reader.GetOrdinal("ApeLLido")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    ImagenPortada = reader.IsDBNull(reader.GetOrdinal("ImagenPortada")) ? null : reader.GetString(reader.GetOrdinal("ImagenPortada")),
                    Progreso = reader.GetDecimal(reader.GetOrdinal("Progreso")),
                    CalificacionFinal = reader.IsDBNull(reader.GetOrdinal("CalificacionFinal")) ? 0 : reader.GetDecimal(reader.GetOrdinal("CalificacionFinal")),
                    EsCompletado = reader.GetBoolean(reader.GetOrdinal("EsCompletado")),
                    FechaInscripcion = reader.GetDateTime(reader.GetOrdinal("FechaInscripcion")),
                    FechaFinalizacion = reader.IsDBNull(reader.GetOrdinal("FechaFinalizacion")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaFinalizacion")),
                });
            }

            await reader.NextResultAsync();
            var recursos = new List<RecursoCalificacionDTO>();
            while (await reader.ReadAsync())
            {
                recursos.Add(new RecursoCalificacionDTO
                {
                    UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                    ModuloRecursoId = reader.GetInt32(reader.GetOrdinal("ModuloRecursoId")),
                    Ponderacion = reader.GetInt32(reader.GetOrdinal("Ponderacion")),
                    TipoRecurso = reader.IsDBNull(reader.GetOrdinal("TipoRecurso")) ? 0 : reader.GetInt32(reader.GetOrdinal("TipoRecurso")),
                    RecursoId = reader.IsDBNull(reader.GetOrdinal("RecursoId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RecursoId")),
                    NombreRecurso = reader.IsDBNull(reader.GetOrdinal("NombreRecurso")) ? "—" : reader.GetString(reader.GetOrdinal("NombreRecurso")),
                    Calificacion = reader.IsDBNull(reader.GetOrdinal("Calificacion")) ? null : reader.GetDecimal(reader.GetOrdinal("Calificacion")),
                    EsPresencial = reader.GetBoolean(reader.GetOrdinal("EsPresencial")),
                    EsScorm = reader.GetBoolean(reader.GetOrdinal("EsScorm")),
                    TieneAsistencia = reader.GetBoolean(reader.GetOrdinal("TieneAsistencia")),
                    NombreTipoRecurso = reader.IsDBNull(reader.GetOrdinal("NombreTipoRecurso"))
                        ? "—"
                        : reader.GetString(reader.GetOrdinal("NombreTipoRecurso")),
                });
            }

            var mapaRecursos = recursos
                .GroupBy(r => r.UsuarioId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var p in participantes)
                p.Recursos = mapaRecursos.TryGetValue(p.UsuarioId, out var r) ? r : new();

            return participantes;
        }

        public async Task<IEnumerable<RespuestaAlumnoDTO>> ObtenerRespuestasAlumno(int evaluacionId, int usuarioId)
        {
            Console.WriteLine($"[DEBUG] ObtenerRespuestasAlumno - EvaluacionId: {evaluacionId}, UsuarioId: {usuarioId}");

            var connectionString = _context.Database.GetConnectionString();
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(
                "EXEC dbo.sp_ObtenerRespuestasAlumnoEvaluacion @EvaluacionId, @UsuarioId", conn);
            cmd.Parameters.AddWithValue("@EvaluacionId", evaluacionId);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

            using var reader = await cmd.ExecuteReaderAsync();
            var resultado = new List<RespuestaAlumnoDTO>();

            Console.WriteLine($"[DEBUG] Reader abierto, leyendo filas...");

            int fila = 0;
            while (await reader.ReadAsync())
            {
                try
                {
                    resultado.Add(new RespuestaAlumnoDTO
                    {
                        RespuestaId = reader.GetInt32(reader.GetOrdinal("RespuestaId")),
                        PreguntaId = reader.IsDBNull(reader.GetOrdinal("PreguntaId")) ? null : reader.GetInt32(reader.GetOrdinal("PreguntaId")),
                        EvaluacionPreguntaId = reader.IsDBNull(reader.GetOrdinal("EvaluacionPreguntaId")) ? null : reader.GetInt32(reader.GetOrdinal("EvaluacionPreguntaId")),
                        TextoPregunta = reader.IsDBNull(reader.GetOrdinal("TextoPregunta")) ? null : reader.GetString(reader.GetOrdinal("TextoPregunta")),
                        TipoPreguntaId = reader.IsDBNull(reader.GetOrdinal("TipoPreguntaId")) ? 0 : reader.GetInt32(reader.GetOrdinal("TipoPreguntaId")),
                        PuntosValor = reader.IsDBNull(reader.GetOrdinal("PuntosValor")) ? 0 : reader.GetDecimal(reader.GetOrdinal("PuntosValor")),
                        OpcionId = reader.IsDBNull(reader.GetOrdinal("OpcionId")) ? null : reader.GetInt32(reader.GetOrdinal("OpcionId")),
                        TextoOpcionSeleccionada = reader.IsDBNull(reader.GetOrdinal("TextoOpcionSeleccionada")) ? null : reader.GetString(reader.GetOrdinal("TextoOpcionSeleccionada")),
                        OpcionEsCorrecta = reader.IsDBNull(reader.GetOrdinal("OpcionEsCorrecta")) ? null : reader.GetBoolean(reader.GetOrdinal("OpcionEsCorrecta")),
                        EsCorrecta = !reader.IsDBNull(reader.GetOrdinal("EsCorrecta")) && reader.GetBoolean(reader.GetOrdinal("EsCorrecta")),
                        PuntosObtenidos = reader.IsDBNull(reader.GetOrdinal("PuntosObtenidos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("PuntosObtenidos")),
                        Intento = reader.IsDBNull(reader.GetOrdinal("Intento")) ? 0 : reader.GetInt32(reader.GetOrdinal("Intento")),
                        FechaRespuesta = reader.IsDBNull(reader.GetOrdinal("FechaRespuesta")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaRespuesta")),
                        TextoRespuesta = reader.IsDBNull(reader.GetOrdinal("TextoRespuesta")) ? null : reader.GetString(reader.GetOrdinal("TextoRespuesta")),
                    });
                    fila++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Fallo al mapear fila: {ex.Message}");
                    Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"[DEBUG] Columna {i}: {reader.GetName(i)} = {(reader.IsDBNull(i) ? "NULL" : reader.GetValue(i)?.ToString())}");
                    }
                }
            }
            Console.WriteLine($"[DEBUG] Total filas leídas: {fila}");

            return resultado;
        }

        public async Task<OperationResult> GuardarRespuestasEvaluacion(GuardarRespuestasDTO dto)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(dto.Respuestas);

            Console.WriteLine($"[DEBUG] GuardarRespuestas - EvaluacionId: {dto.EvaluacionId}, UsuarioId: {dto.UsuarioId}");
            Console.WriteLine($"[DEBUG] JSON enviado: {json}");

            var connectionString = _context.Database.GetConnectionString();
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(
                "EXEC dbo.sp_GuardarRespuestasEvaluacion @EvaluacionId, @UsuarioId, @Respuestas", conn);
            cmd.Parameters.AddWithValue("@EvaluacionId", dto.EvaluacionId);
            cmd.Parameters.AddWithValue("@UsuarioId", dto.UsuarioId);
            cmd.Parameters.Add(new SqlParameter("@Respuestas", SqlDbType.NVarChar, -1) { Value = json });

            try
            {
                using var reader = await cmd.ExecuteReaderAsync();
                int intento = 0;
                if (await reader.ReadAsync())
                    intento = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);

                Console.WriteLine($"[DEBUG] Respuestas guardadas, intento: {intento}");

                if (intento == 0)
                    return OperationResult.Fail("SP no devolvió intento — posible fallo silencioso");

                return OperationResult.Ok(intento, "Respuestas evaluacion guardadas.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GuardarRespuestas: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                return OperationResult.Fail($"Error al guardar respuestas: {ex.Message}");
            }
        }

        public async Task<bool> VerificarInscripcion(int usuarioId, int cursoId)
        {
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", usuarioId),
                new SqlParameter("@CursoId", cursoId)
            };

            var resultado = await _context.Database
                .SqlQueryRaw<int>("EXEC sp_VerificarInscripcion @UsuarioId, @CursoId", parametros)
                .ToListAsync();

            return resultado.FirstOrDefault() == 1;
        }
    }
}
