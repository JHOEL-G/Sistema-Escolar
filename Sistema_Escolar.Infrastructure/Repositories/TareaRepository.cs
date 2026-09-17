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
using System.Text.Json;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class TareaRepository : IRecursoTareaRepository
    {
        private readonly ConfiaContext _context;

        public TareaRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoTarea(RecursoTareaDTO dto)
        {
            if (dto.ModuloRecursoId <= 0)
                return OperationResult.Fail("ModuloRecursoId es requerido.");
            if (string.IsNullOrWhiteSpace(dto.Instrucciones))
                return OperationResult.Fail("Las instrucciones son requeridas.");

            string? archivosJson = dto.Archivos != null && dto.Archivos.Count > 0
                ? JsonSerializer.Serialize(dto.Archivos)
                : null;

            var parametross = new[]
            {
                new SqlParameter("@ModuloRecursoId",    dto.ModuloRecursoId),
                new SqlParameter("@Instrucciones",      (object?)dto.Instrucciones      ?? DBNull.Value),
                new SqlParameter("@Descripcion",        (object?)dto.Descripcion        ?? DBNull.Value), 
                new SqlParameter("@AgregarPonderacion", dto.AgregarPonderacion),
                new SqlParameter("@Privacidad",         (object?)dto.Privacidad         ?? DBNull.Value),
                new SqlParameter("@TipoCalificacionId", (object?)dto.TipoCalificacionId ?? DBNull.Value),
                new SqlParameter("@ArchivoPath",        DBNull.Value),  
                new SqlParameter("@Archivos",           (object?)archivosJson ?? DBNull.Value)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                    "EXEC sp_CrearRecursoTarea @ModuloRecursoId, @Instrucciones, @Descripcion, @AgregarPonderacion, @Privacidad, @TipoCalificacionId, @ArchivoPath, @Archivos",
                parametross).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Datos no guardados en la base de datos.");
            return sp.ResultId > 0 ? OperationResult.Ok(sp.ResultId, sp.Mensaje) : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> EntregarTarea(EntregarTareaDTO dto)
        {
            var parametros = new[]
    {
        new SqlParameter("@TareaId",     dto.TareaId),
        new SqlParameter("@UsuarioId",   dto.UsuarioId),
        new SqlParameter("@Titulo",      (object?)dto.Titulo     ?? DBNull.Value),
        new SqlParameter("@Comentario",  (object?)dto.Comentario ?? DBNull.Value),
        new SqlParameter("@ArchivoPath", (object?)dto.Archivo    ?? DBNull.Value)  
    };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_EntregarTarea @TareaId, @UsuarioId, @Titulo, @Comentario, @ArchivoPath",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Datos no guardados en la base de datos.");
            return sp.ResultId > 0 ? OperationResult.Ok(sp.ResultId, sp.Mensaje) : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> GetTareaEntregados(int tarea, int usuario)
        {
            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
        "EXEC sp_ObtenerEntregaTarea @TareaId, @UsuarioId",
        new SqlParameter("@TareaId", tarea),
        new SqlParameter("@UsuarioId", usuario)).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null || sp.ResultId == 0) return OperationResult.Fail("No se encontró entrega.");
            return sp.ResultId > 0 ? OperationResult.Ok(sp.Mensaje) : OperationResult.Fail(sp.Mensaje);
        }
    }
}
