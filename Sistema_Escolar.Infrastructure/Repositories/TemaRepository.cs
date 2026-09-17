using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class TemaRepository : ITemaRepository
    {
        private readonly ConfiaContext _context;

        public TemaRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateTema(TtemaDTO ttemaDTO)
        {
            string subtemasString = ttemaDTO.CreacionSubtema != null
                ? string.Join(", ", ttemaDTO.CreacionSubtema)
                : string.Empty;

            var parametros = new[]
            {
                new SqlParameter("@NombreTema", ttemaDTO.NombreTema),
                new SqlParameter("@Descripcion", ttemaDTO.Descripcion),
                new SqlParameter("@ImagenPortada", ttemaDTO.ImagenPortada ?? (object)DBNull.Value),
                new SqlParameter("@CreacionSubtema", subtemasString)
            };

            var resulato = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_Crear_Tema @NombreTema, @Descripcion, @ImagenPortada, @CreacionSubtema", parametros).ToListAsync();

            var sp = resulato.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al ejecutar el procedimiento almacenado.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<IEnumerable<TtemaDTO?>> GetTtemaId(int id)
        {
            var resultado = await _context.Database.SqlQueryRaw<TtemaDTO>(
                "EXEC sp_ObtenerTemaId @TemaId",
                new SqlParameter("@TemaId", id)
            ).ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<TtemaDTO>> GetTtemas()
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var resultado = await connection.QueryAsync<TtemaDTO>(
                    "sp_ObtenerTodoTema",
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return resultado;
            }
        }

        public async Task<OperationResult> UpdateTema(TtemaDTO ttemaDTO)
        {
            var parametros = new[]
            {
                new SqlParameter("@TemaId", ttemaDTO.TemaId),
                new SqlParameter("@NombreTema", ttemaDTO.NombreTema),
                new SqlParameter("@Descripcion", ttemaDTO.Descripcion),
                new SqlParameter("@ImagenPortada", ttemaDTO.ImagenPortada ?? (object)DBNull.Value),
                new SqlParameter("@CreacionSubtema", ttemaDTO.CreacionSubtema != null
                    ? string.Join(", ", ttemaDTO.CreacionSubtema)
                    : string.Empty)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_Modificar_Tema @TemaId, @NombreTema, @Descripcion, @ImagenPortada, @CreacionSubtema", parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al actualizar el tema");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
