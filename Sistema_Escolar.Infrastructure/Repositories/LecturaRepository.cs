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
    public class LecturaRepository : IRecursoLecturaRepository
    {
        private readonly ConfiaContext _context;

        public LecturaRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoLectura(RecursoLecturaDTO dto)
        {
            var archivosTable = new DataTable();
            archivosTable.Columns.Add("NombreArchivo", typeof(string));
            archivosTable.Columns.Add("RutaArchivo", typeof(string));
            archivosTable.Columns.Add("TipoArchivo", typeof(string));
            archivosTable.Columns.Add("TamañoMB", typeof(decimal));

            if (dto.ArchivosAdjuntos != null && dto.ArchivosAdjuntos.Any())
            {
                foreach (var archivo in dto.ArchivosAdjuntos)
                {
                    archivosTable.Rows.Add(
                        archivo.NombreArchivo ?? "",
                        archivo.RutaArchivo ?? "",
                        archivo.TipoArchivo ?? "",
                        (object?)archivo.TamañoMB ?? DBNull.Value
                    );
                }
            }

            var connectionString = _context.Database.GetConnectionString();
            await using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new SqlCommand("sp_CrearRecursoLectura", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ModuloRecursoId", dto.ModuloRecursoId);
            cmd.Parameters.AddWithValue("@TipoLecturaId", dto.TipoLecturaId);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)dto.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ContenidoHTML", (object?)dto.ContenidoHTML ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ArchivoPDFPath", (object?)dto.ArchivoPDFPath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NombreArchivoPDF", (object?)dto.NombreArchivoPDF ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HacerVisibleDashboard", dto.HacerVisibleDashboard);
            cmd.Parameters.Add(new SqlParameter("@ArchivosAdjuntos", archivosTable)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.TipoArchivosAdjuntos"
            });

            Console.WriteLine($"[LECTURA-REPO] ModuloRecursoId={dto.ModuloRecursoId}, TipoLecturaId={dto.TipoLecturaId}, ArchivoPDFPath='{dto.ArchivoPDFPath}', Adjuntos={archivosTable.Rows.Count}");

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                int resultId = reader.GetInt32(reader.GetOrdinal("ResultId"));
                string mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));

                Console.WriteLine($"[LECTURA-REPO] SP respondió: ResultId={resultId}, Mensaje='{mensaje}'");

                return resultId > 0
                    ? OperationResult.Ok(resultId, mensaje)
                    : OperationResult.Fail(mensaje);
            }

            return OperationResult.Fail("El SP no devolvió ningún resultado");
        }
    }
}
