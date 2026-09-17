using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ConfiaContext _context;
        public RolRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<CatRolDTO?> GetRolById(int id)
        {
            var resultado = await _context.Database.SqlQueryRaw<CatRolDTO>(
                "EXEC sp_ObtenerRol @RolId",
                new SqlParameter("@RolId", id)).ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<CatRolDTO>> GetRoles()
        {
            var resultado = await _context.Database.SqlQueryRaw<CatRolDTO>(
                "EXEC sp_ObtenerTodosRol").ToListAsync();

            return resultado;
        }

        public async Task<OperationResult> InsertarRol(CatRolDTO catRol)
        {
            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_InsertarRol @NombreRol, @Descripcion",
                new SqlParameter("@NombreRol", catRol.NombreRol),
                new SqlParameter("@Descripcion", (object)catRol.Descripcion ?? DBNull.Value)).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> UpdatearRol(CatRolDTO catRol)
        {
            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult> (
                "EXEC sp_ModificarRol @RolId, @NombreRol, @Descripcion",
                new SqlParameter("@RolId", catRol.RolId),
                new SqlParameter("@NombreRol", catRol.NombreRol),
                new SqlParameter("@Descripcion",(object)catRol.Descripcion ?? DBNull.Value)).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
