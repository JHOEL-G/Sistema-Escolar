using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class GestionDocumentoRepository : IGestionDocumentoRepository
    {
        private readonly ConfiaContext _context;

        public GestionDocumentoRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateCarpeta(CarpetaInsertarDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@NombreCarpeta", dto.NombreCarpeta ?? (object)DBNull.Value),
                new SqlParameter("@PadreId", dto.PadreId ?? (object)DBNull.Value)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_InsertarCarpeta @NombreCarpeta, @PadreId", parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("No se guardaron los datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> CreateDocumento(DocumentoInsertarDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@TituloDocumento", dto.TituloDocumento),
                new SqlParameter("@Descripcion", dto.Descripcion),
                new SqlParameter("@DocumentoPath", dto.DocumentoPath),
                new SqlParameter("@FechaInicio", dto.FechaInicio),
                new SqlParameter("@FechaExpiracion", dto.FechaExpiracion),
                new SqlParameter("@ExploradorId", dto.ExploradorId),
                new SqlParameter("@Formatos", dto.Formatos),
                new SqlParameter("@Roles", dto.Roles)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_InsertarDocumento @TituloDocumento, @Descripcion, @DocumentoPath, @FechaInicio, @FechaExpiracion, @ExploradorId, @Formatos, @Roles", parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("No se guardaron los datos en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> DeleteCarpeta(int exploradorId)
        {
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EliminarCarpeta @ExploradorId",
                new SqlParameter("@ExploradorId", exploradorId));

            var sp = resultado;

            if (sp == 0) return OperationResult.Fail("No se pudo eliminar la carpeta");

            return resultado > 0
                ? OperationResult.Ok("Carpeta eliminada")
                : OperationResult.Fail("No se pudo eliminar la carpeta");
        }

        public async Task<OperationResult> DeleteDocumento(int gestionId)
        {
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EliminarDocumento @GestionId",
                new SqlParameter("@GestionId", gestionId));

            var sp = resultado;

            if (sp == 1) return OperationResult.Fail("No se eliminaron los datos");

            return sp > 0
                ? OperationResult.Ok("Documento eliminado")
                : OperationResult.Fail("No se eliminaron los datos");
        }

        public async Task<OperationResult<List<CarpetaResponseDTO>>> GetArbolCarpetas()
        {
            var resultado = await _context.Database.SqlQueryRaw<CarpetaResponseDTO>(
                "EXEC sp_ObtenerArbolCarpetas").ToListAsync();

            return OperationResult<List<CarpetaResponseDTO>>.Ok(resultado);
        }

        public async Task<OperationResult<List<DocumentoResponseDTO>>> GetDocumentosPorCarpeta(int exploradorId)
        {
            var resultado = await _context.Database.SqlQueryRaw<DocumentoResponseDTO>(
                "EXEC sp_ListarDocumentosPorCarpeta @ExploradorId",
                new SqlParameter("@ExploradorId", exploradorId)).ToListAsync();

            return OperationResult<List<DocumentoResponseDTO>>.Ok(resultado);
        }

        public async Task<OperationResult> UpdateCarpeta(CarpetaActualizarDTO dto)
        {
            var parametros = new[]
           {
                new SqlParameter("@ExploradorId", dto.ExploradorId),
                new SqlParameter("@NombreCarpeta", dto.NombreCarpeta)
            };

            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarCarpeta @ExploradorId, @NombreCarpeta", parametros);

            return resultado > 0
                ? OperationResult.Ok("Carpeta actualizada exitosamente")
                : OperationResult.Fail("No se pudo actualizar la carpeta");
        }

        public async Task<OperationResult> UpdateDocumento(DocumentoActualizarDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@GestionId", dto.GestionId),
                new SqlParameter("@TituloDocumento", dto.TituloDocumento),
                new SqlParameter("@Descripcion", dto.Descripcion),
                new SqlParameter("@DocumentoPath", dto.DocumentoPath),
                new SqlParameter("@FechaInicio", dto.FechaInicio),
                new SqlParameter("@FechaExpiracion", dto.FechaExpiracion),
                new SqlParameter("@ExploradorId", dto.ExploradorId),
                new SqlParameter("@Activo", dto.Activo),
                new SqlParameter("@Formatos", dto.Formatos),
                new SqlParameter("@Roles", dto.Roles)
            };

            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarDocumento @GestionId, @TituloDocumento, @Descripcion, @DocumentoPath, @FechaInicio, @FechaExpiracion, @ExploradorId, @Activo, @Formatos, @Roles",
                parametros);

            return resultado > 0
                ? OperationResult.Ok("Documento actualizado exitosamente")
                : OperationResult.Fail("No se pudo actualizar el documento");
        }
    }
}
