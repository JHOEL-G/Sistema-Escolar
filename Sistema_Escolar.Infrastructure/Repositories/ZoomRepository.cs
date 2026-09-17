using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using Sistema_Escolar_Confia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class ZoomRepository : IRecursoZoomRepository
    {
        private readonly ConfiaContext _context;

        public ZoomRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoZoom(RecursoZoomDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId", dto.ModuloRecursoId),
                new SqlParameter("@EnlaceZoom", string.IsNullOrEmpty(dto.EnlaceZoom) ? (object)DBNull.Value : dto.EnlaceZoom),
                new SqlParameter("@Descripcion", dto.Descripcion ?? (object)DBNull.Value),  
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                   "EXEC sp_CrearRecursoZoom @ModuloRecursoId, @EnlaceZoom, @Descripcion", 
    parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Datos no guardados en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId,sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
