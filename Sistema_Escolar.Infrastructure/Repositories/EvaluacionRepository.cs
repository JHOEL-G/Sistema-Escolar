using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class EvaluacionRepository : IRecursoEvaluacionRepository
    {
        private readonly ConfiaContext _context;

        public EvaluacionRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoEvaluacion(RecursoEvaluacionDTO dto)
        {
            if (dto.ModuloRecursoId <= 0)
                return OperationResult.Fail("ModuloRecursoId es requerido.");
            if (string.IsNullOrWhiteSpace(dto.Instrucciones))
                return OperationResult.Fail("Las instrucciones son requeridas.");

            var bancasTable = new DataTable();
            bancasTable.Columns.Add("Id", typeof(int));
            var bancas = dto.BancasIds ?? new List<BancaPreguntaSeleccionadaDTO>();
            foreach (var b in bancas)
                bancasTable.Rows.Add(b.BancaId);

            var preguntasTable = new DataTable();
            preguntasTable.Columns.Add("EvaluacionPreguntaId", typeof(int));
            preguntasTable.Columns.Add("TextoPregunta", typeof(string));
            preguntasTable.Columns.Add("TipoPreguntaId", typeof(int));
            preguntasTable.Columns.Add("PuntosValor", typeof(decimal));
            preguntasTable.Columns.Add("Activo", typeof(bool));
            preguntasTable.Columns.Add("ImagenPregunta", typeof(string));

            var opcionesTable = new DataTable();
            opcionesTable.Columns.Add("EvaluacionOpcionId", typeof(int));
            opcionesTable.Columns.Add("PreguntaOrden", typeof(int));
            opcionesTable.Columns.Add("TextoOpcion", typeof(string));
            opcionesTable.Columns.Add("EsCorrecta", typeof(bool));
            opcionesTable.Columns.Add("ExplicacionORelacion", typeof(string));
            opcionesTable.Columns.Add("ImagenOpcion", typeof(string));

            int orden = 0;
            foreach (var p in dto.Preguntas ?? new List<PreguntaManualDTO>())
            {
                preguntasTable.Rows.Add(
                    p.EvaluacionPreguntaId.HasValue ? (object)p.EvaluacionPreguntaId.Value : DBNull.Value,
                    p.TextoPregunta ?? "",
                    p.TipoPreguntaId,
                    (decimal)(p.PuntosValor),
                    p.Activo,
                    p.ImagenPregunta ?? (object)DBNull.Value
                );

                foreach (var o in p.Opciones ?? new List<OpcionManualDTO>())
                {
                    opcionesTable.Rows.Add(
                            o.EvaluacionOpcionId.HasValue ? (object)o.EvaluacionOpcionId.Value : DBNull.Value,
                        orden,
                        o.TextoOpcion ?? "",
                        o.EsCorrecta,
                        o.ExplicacionORelacion ?? "",
                        o.ImagenOpcion ?? (object)DBNull.Value
                    );
                }
                orden++;
            }

            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId",           dto.ModuloRecursoId),
                new SqlParameter("@Instrucciones", dto.Instrucciones),
                new SqlParameter("@Descripcion",               dto.Descripcion ?? (object)DBNull.Value), 
                new SqlParameter("@TipoCalificacionId",        dto.TipoCalificacionId),
                new SqlParameter("@AgregarPonderacion", dto.AgregarPonderacion ?? false),
                new SqlParameter("@PreguntasAleatorias", dto.PreguntasAleatorias ?? false),
                new SqlParameter("@Oportunidades",             dto.Oportunidades),
                new SqlParameter("@PermitirReinicio",          dto.PermitirReinicio ?? (object)DBNull.Value),
                new SqlParameter("@PreguntasCorrectasAprobar", dto.PreguntasCorrectasAprobar),
                new SqlParameter("@TiempoHoras",               dto.TiempoHoras),
                new SqlParameter("@TiempoMinutos",             dto.TiempoMinutos),
                new SqlParameter("@BancasPreguntas", bancasTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoListaIds"
                },
                new SqlParameter("@Preguntas", preguntasTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoPreguntaManual"
                },
                new SqlParameter("@Opciones", opcionesTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoOpcionManual"
                }
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
            "EXEC sp_CrearRecursoEvaluacion " +
            "@ModuloRecursoId, @Instrucciones, @Descripcion, @TipoCalificacionId, @AgregarPonderacion, " +
            "@PreguntasAleatorias, @Oportunidades, @PermitirReinicio, @PreguntasCorrectasAprobar, " +
            "@TiempoHoras, @TiempoMinutos, @BancasPreguntas, @Preguntas, @Opciones",
            parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("No se pudo ejecutar el procedimiento.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> UpdateRecursoEvaluacion(RecursoEvaluacionDTO dto)
        {
            if (dto.ModuloRecursoId <= 0)
                return OperationResult.Fail("ModuloRecursoId es requerido.");
            if (string.IsNullOrWhiteSpace(dto.Instrucciones))
                return OperationResult.Fail("Las instrucciones son requeridas.");

            var bancasTable = new DataTable();
            bancasTable.Columns.Add("Id", typeof(int));
            var bancas = dto.BancasIds ?? new List<BancaPreguntaSeleccionadaDTO>();
            foreach (var b in bancas)
                bancasTable.Rows.Add(b.BancaId);

            var preguntasTable = new DataTable();
            preguntasTable.Columns.Add("EvaluacionPreguntaId", typeof(int)); 
            preguntasTable.Columns.Add("TextoPregunta", typeof(string));
            preguntasTable.Columns.Add("TipoPreguntaId", typeof(int));
            preguntasTable.Columns.Add("PuntosValor", typeof(decimal));
            preguntasTable.Columns.Add("Activo", typeof(bool));
            preguntasTable.Columns.Add("ImagenPregunta", typeof(string));

            var opcionesTable = new DataTable();
            opcionesTable.Columns.Add("PreguntaOrden", typeof(int));
            opcionesTable.Columns.Add("TextoOpcion", typeof(string));
            opcionesTable.Columns.Add("EsCorrecta", typeof(bool));
            opcionesTable.Columns.Add("ExplicacionORelacion", typeof(string));
            opcionesTable.Columns.Add("ImagenOpcion", typeof(string));

            int orden = 0;
            foreach (var p in dto.Preguntas ?? new List<PreguntaManualDTO>())
            {
                preguntasTable.Rows.Add(
                        p.EvaluacionPreguntaId.HasValue ? (object)p.EvaluacionPreguntaId.Value : DBNull.Value,
                    p.TextoPregunta ?? "",
                    p.TipoPreguntaId,
                    (decimal)(p.PuntosValor),
                    p.Activo,
                    p.ImagenPregunta ?? (object)DBNull.Value
                );

                foreach (var o in p.Opciones ?? new List<OpcionManualDTO>())
                {
                    opcionesTable.Rows.Add(
                        orden,
                        o.TextoOpcion ?? "",
                        o.EsCorrecta,
                        o.ExplicacionORelacion ?? "",
                        o.ImagenOpcion ?? (object)DBNull.Value
                    );
                }
                orden++;
            }

            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId",           dto.ModuloRecursoId),
                new SqlParameter("@Instrucciones", dto.Instrucciones),
                new SqlParameter("@Descripcion",               dto.Descripcion ?? (object)DBNull.Value), 
                new SqlParameter("@TipoCalificacionId",        dto.TipoCalificacionId),
                new SqlParameter("@AgregarPonderacion", dto.AgregarPonderacion ?? false),
                new SqlParameter("@PreguntasAleatorias", dto.PreguntasAleatorias ?? false),
                new SqlParameter("@Oportunidades",             dto.Oportunidades),
                new SqlParameter("@PermitirReinicio",          dto.PermitirReinicio ?? (object)DBNull.Value),
                new SqlParameter("@PreguntasCorrectasAprobar", dto.PreguntasCorrectasAprobar),
                new SqlParameter("@TiempoHoras",               dto.TiempoHoras),
                new SqlParameter("@TiempoMinutos",             dto.TiempoMinutos),
                new SqlParameter("@BancasPreguntas", bancasTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoListaIds"
                },
                new SqlParameter("@Preguntas", preguntasTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoPreguntaManual"
                },
                new SqlParameter("@Opciones", opcionesTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoOpcionManual"
                }
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_ActualizarRecursoEvaluacion " +
                "@ModuloRecursoId, @Instrucciones, @Descripcion, @TipoCalificacionId, @AgregarPonderacion, " +
                "@PreguntasAleatorias, @Oportunidades, @PermitirReinicio, @PreguntasCorrectasAprobar, " +
                "@TiempoHoras, @TiempoMinutos, @BancasPreguntas, @Preguntas, @Opciones",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("No se pudo ejecutar el procedimiento.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
