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
    public class ScormRepository : IRecursoScormRepository
    {
        private readonly ConfiaContext _context;

        public ScormRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoScorm(RecursoScormDTO dto)
        {
            if (dto.ModuloRecursoId <= 0)
                return OperationResult.Fail("ModuloRecursoId es requerido.");
            if (string.IsNullOrWhiteSpace(dto.NombreArchivo))
                return OperationResult.Fail("El nombre del archivo es requerido.");

            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId", dto.ModuloRecursoId),
                new SqlParameter("@ArchivoPath", dto.ArchivoPath ?? (object)DBNull.Value),
                new SqlParameter("@NombreArchivo",                dto.NombreArchivo), 
                new SqlParameter("@TamañoMB", dto.TamañoMB.HasValue ? (object)dto.TamañoMB.Value : DBNull.Value),
                new SqlParameter("@AgregarPonderacion", dto.AgregarPonderacion.HasValue ? (object)dto.AgregarPonderacion.Value : DBNull.Value),
                new SqlParameter("@PermitirModoPantallaCompleta", dto.PermitirModoPantallaCompleta.HasValue ? (object)dto.PermitirModoPantallaCompleta.Value : DBNull.Value),
                new SqlParameter("@TipoCalificacionId", dto.TipoCalificacionId),
                new SqlParameter("@Descripcion", dto.Descripcion ?? (object)DBNull.Value), 
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                 "EXEC sp_CrearRecursoScorm @ModuloRecursoId, @ArchivoPath, @NombreArchivo, @TamañoMB, @AgregarPonderacion, @PermitirModoPantallaCompleta, @TipoCalificacionId, @Descripcion",  // ✅ nuevo
    parametros).ToListAsync();
            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Datos no guardados en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
