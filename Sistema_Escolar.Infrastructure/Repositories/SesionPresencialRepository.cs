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
    public class SesionPresencialRepository : IRecursoSesionPresencialRepository
    {
        private readonly ConfiaContext _context;

        public SesionPresencialRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoSesionPresencial(RecursoSesionPresencialDTO dto)
        {
            if (dto.FechaSesion == default || dto.FechaSesion < new DateTime(1753, 1, 1))
                return OperationResult.Fail("La fecha de la sesión es obligatoria y debe ser válida.");

            var parametros = new[]
            {
        new SqlParameter("@ModuloRecursoId", dto.ModuloRecursoId),
        new SqlParameter("@FechaSesion",     dto.FechaSesion),
        new SqlParameter("@Lugar",           dto.Lugar ?? (object)DBNull.Value),
        new SqlParameter("@Duracion",        dto.Duracion),
        new SqlParameter("@Descripcion",     dto.Descripcion  ?? (object)DBNull.Value),
        new SqlParameter("@Direccion",       dto.Direccion    ?? (object)DBNull.Value),
        new SqlParameter("@Instrucciones",   dto.Instrucciones ?? (object)DBNull.Value),
        new SqlParameter("@HoraFin",         dto.HoraFin.HasValue ? (object)dto.HoraFin.Value : DBNull.Value)
    };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_CrearRecursoSesionPresencial @ModuloRecursoId, @FechaSesion, @Lugar, @Duracion, @Descripcion, @Direccion, @Instrucciones, @HoraFin",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Datos no guardados en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
