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
    public class EvaluacionPresencialRepository : IRecursoEvaluacionPresencialRepository
    {
        private readonly ConfiaContext _context;

        public EvaluacionPresencialRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoEvaluacionPresencial(RecursoEvaluacionPresencialDTO dto)
        {
            Console.WriteLine($"[EVAL-PRES-REPO] Iniciando ModuloRecursoId={dto.ModuloRecursoId}");
            try
            {
                var rubricasTable = new DataTable();
                rubricasTable.Columns.Add("Nombre", typeof(string));
                rubricasTable.Columns.Add("Descripcion", typeof(string));
                foreach (var r in dto.Rubricas ?? new List<RubricaPresencialDTO>())
                    rubricasTable.Rows.Add(r.Nombre, r.Descripcion ?? (object)DBNull.Value);

                var criteriosTable = new DataTable();
                criteriosTable.Columns.Add("RubricaOrden", typeof(int));
                criteriosTable.Columns.Add("TituloCriterio", typeof(string));
                criteriosTable.Columns.Add("Descripcion", typeof(string));
                criteriosTable.Columns.Add("OrdenCriterio", typeof(int));
                foreach (var c in dto.Criterios ?? new List<CriterioPresencialDTO>())
                    criteriosTable.Rows.Add(c.RubricaOrden, c.TituloCriterio, c.Descripcion ?? (object)DBNull.Value, c.OrdenCriterio);

                var calificacionesTable = new DataTable();
                calificacionesTable.Columns.Add("RubricaOrden", typeof(int));
                calificacionesTable.Columns.Add("CriterioOrden", typeof(int));
                calificacionesTable.Columns.Add("Nombre", typeof(string));
                calificacionesTable.Columns.Add("Puntos", typeof(decimal));
                calificacionesTable.Columns.Add("OrdenCalificacion", typeof(int));
                foreach (var cal in dto.Calificaciones ?? new List<CalificacionPresencialDTO>())
                    calificacionesTable.Rows.Add(cal.RubricaOrden, cal.CriterioOrden, cal.Nombre, cal.Puntos, cal.OrdenCalificacion);

                var connectionString = _context.Database.GetConnectionString();
                await using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                await using var cmd = new SqlCommand("sp_CrearRecursoEvaluacionPresencial", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuloRecursoId", dto.ModuloRecursoId);
                cmd.Parameters.AddWithValue("@AgregarPonderacion", dto.AgregarPonderacion);
                cmd.Parameters.AddWithValue("@ColaboradorSolicitarRevision", dto.ColaboradorSolicitarRevision);
                cmd.Parameters.AddWithValue("@TipoCalificacionId", dto.TipoCalificacionId);
                cmd.Parameters.AddWithValue("@Descripcion", dto.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.Add(new SqlParameter("@Rubricas", rubricasTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.TipoRubricaPresencial"
                });
                cmd.Parameters.Add(new SqlParameter("@Criterios", criteriosTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.TipoCriterioPresencial"
                });
                cmd.Parameters.Add(new SqlParameter("@Calificaciones", calificacionesTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.TipoCalificacionPresencial"
                });

                Console.WriteLine($"[EVAL-PRES-REPO] Ejecutando SP...");
                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int resultId = reader.GetInt32(reader.GetOrdinal("ResultId"));
                    string mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                    Console.WriteLine($"[EVAL-PRES-REPO] ResultId={resultId}, Mensaje={mensaje}");
                    return resultId > 0
                        ? OperationResult.Ok(resultId, mensaje)
                        : OperationResult.Fail(mensaje);
                }
                return OperationResult.Fail("El SP no devolvió ningún resultado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EVAL-PRES-REPO] ERROR: {ex.Message}");
                Console.WriteLine($"[EVAL-PRES-REPO] Inner: {ex.InnerException?.Message}");
                return OperationResult.Fail($"Error: {ex.Message}");
            }
        }
    }
}
