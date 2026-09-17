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
    public class OuRepository : IOuRespository
    {
        private readonly ConfiaContext _context;

        public OuRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OuDTO>> GetOu()
        {
            var resultado = await _context.Database.SqlQueryRaw<OuDTO>(
                "EXEC sp_ObtenerTodoOU").ToListAsync();

            return resultado;
        }

        public async Task<OuDTO?> GetOuById(int id)
        {
            var resultado = await _context.Database.SqlQueryRaw<OuDTO>(
                "EXEC sp_ObtenerOU @OrganizacionalesId",
                new SqlParameter("OrganizacionalesId", id)).ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<OperationResult> InsertarOu(OuDTO ou)
        {
            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_InsertarOU @Nombre, @Descripcion, @JefeId",
                new SqlParameter("@Nombre", ou.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@Descripcion", ou.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@JefeId", ou.JefeId ?? (object)DBNull.Value)).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al insertar la ou.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> UpdatearOu(OuDTO ou)
        {
            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_ModificarOU @OrganizacionalesId, @Nombre, @Descripcion, @JefeId",
                new SqlParameter("@OrganizacionalesId", ou.OrganizacionalesId),
                new SqlParameter("@Nombre", ou.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@Descripcion", ou.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@JefeId", ou.JefeId ?? (object)DBNull.Value)).ToListAsync();
            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al actualizar la OU.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
