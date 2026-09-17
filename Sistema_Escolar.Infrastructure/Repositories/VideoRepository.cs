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
    public class VideoRepository : IRecursoVideoRepository
    {
        private readonly ConfiaContext _context;

        public VideoRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoVideo(RecursoVideoDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId", dto.ModuloRecursoId),
                new SqlParameter("@VideoPath", dto.VideoPath ?? (object)DBNull.Value),
                new SqlParameter("@VideoLink", dto.VideoLink ?? (object)DBNull.Value),
                new SqlParameter("@TipoSubida", dto.TipoSubida),
                new SqlParameter("@HacerVisibleDashboard", dto.HacerVisibleDashboard),
                new SqlParameter("@Descripcion", dto.Descripcion ?? (object)DBNull.Value),  
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                    "EXEC sp_CrearRecursoVideo @ModuloRecursoId, @VideoPath, @VideoLink, @TipoSubida, @HacerVisibleDashboard, @Descripcion",
    parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al crear el procedimiento almacenado");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
