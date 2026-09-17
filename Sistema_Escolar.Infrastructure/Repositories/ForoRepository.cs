using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class ForoRepository : IRecursoForoRepository
    {
        private readonly ConfiaContext _context;

        public ForoRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoForo(RecursoForoDTO dto)
        {
            if (dto.ModuloRecursoId <= 0)
                return OperationResult.Fail("ModuloRecursoId es requerido.");
            if (string.IsNullOrWhiteSpace(dto.Instrucciones))
                return OperationResult.Fail("Las instrucciones son requeridas.");

            string? archivosJson = dto.Archivos != null && dto.Archivos.Count > 0
                ? JsonSerializer.Serialize(dto.Archivos)
                : null;

            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId",    dto.ModuloRecursoId),
                new SqlParameter("@NombreForo",         (object?)dto.NombreForo         ?? DBNull.Value),
                new SqlParameter("@Instrucciones",      (object?)dto.Instrucciones      ?? DBNull.Value),
                new SqlParameter("@Descripcion",        (object?)dto.Descripcion        ?? DBNull.Value), 
                new SqlParameter("@AgregarPonderacion", dto.AgregarPonderacion),
                new SqlParameter("@Privacidad",         (object?)dto.Privacidad         ?? DBNull.Value),
                new SqlParameter("@TipoCalificacionId", (object?)dto.TipoCalificacionId ?? DBNull.Value),
                new SqlParameter("@ArchivoPath",        DBNull.Value), 
                new SqlParameter("@Archivos",           (object?)archivosJson           ?? DBNull.Value),
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                    "EXEC sp_CrearRecursoForo @ModuloRecursoId, @NombreForo, @Instrucciones, @Descripcion, @AgregarPonderacion, @Privacidad, @TipoCalificacionId, @ArchivoPath, @Archivos",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Error al ejecutar el procedimiento almacenado.");
            return sp.ResultId > 0 ? OperationResult.Ok(sp.ResultId, sp.Mensaje) : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> GetForosPublicado(int foro)
        {
            var resultado = await _context.Database.SqlQueryRaw<PublicarForoDTO>(
                "EXEC sp_ObtenerPublicacionesForo @ForoId",
                new SqlParameter("@ForoId", foro)
            ).ToListAsync();

            return OperationResult.Ok(resultado, "Publicaciones obtenidas");
        }

        public async Task<OperationResult> PublicarForo(PublicarForoDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@ForoId",             dto.ForoId),
                new SqlParameter("@UsuarioId",          dto.UsuarioId),
                new SqlParameter("@Titulo",             (object?)dto.Titulo             ?? DBNull.Value),
                new SqlParameter("@Contenido",          dto.Contenido),
                new SqlParameter("@PublicacionPadreId", (object?)dto.PublicacionPadreId ?? DBNull.Value),
                new SqlParameter("@ArchivoPath",        (object?)dto.ArchivoPath        ?? DBNull.Value),
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_PublicarEnForo @ForoId, @UsuarioId, @Titulo, @Contenido, @PublicacionPadreId, @ArchivoPath",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Error al ejecutar el procedimiento almacenado.");

            if (sp.ResultId == 0 && sp.Mensaje.Contains("Ya existe"))
                return OperationResult.Fail(sp.Mensaje);

            return sp.ResultId > 0 ? OperationResult.Ok(sp.ResultId, sp.Mensaje) : OperationResult.Fail(sp.Mensaje);
        }
    }
}
