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
    public class PropiedadRepository : IPropiedadRepository
    {
        private readonly ConfiaContext _context;

        public PropiedadRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<PropiedadDTO?> GetPropiedadById(int id)
        {
            var resultado = await _context.Database.SqlQueryRaw<PropiedadDTO>(
                "EXEC sp_ObtenerPropiedad @PropiedadId",
                new SqlParameter("@PropiedadId", id)).ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<PropiedadDTO>> GetPropiedades()
        {
            var resultado = await _context.Database.SqlQueryRaw<PropiedadDTO>(
                "EXEC sp_ObtenerTodos_Propiedad").ToListAsync();

            return resultado;
        }

        public async Task<OperationResult> InsertarPropiedad(PropiedadDTO propiedad)
        {
            var parametros = new[]
            {
                new SqlParameter("@NombrePropiedad", propiedad.NombrePropiedad ?? (object)DBNull.Value),
                new SqlParameter("@Etiqueta", propiedad.Etiqueta ?? (object)DBNull.Value),
                new SqlParameter("@TipoCampo", propiedad.TipoCampo ?? (object)DBNull.Value),
                new SqlParameter("@ValorDefecto", propiedad.ValorDefecto ?? (object)DBNull.Value),
                new SqlParameter("@EsRequerido", propiedad.EsRequerido ?? (object)DBNull.Value),
                new SqlParameter("@UsarComoFiltro", propiedad.UsarComoFiltro ?? (object)DBNull.Value),
                new SqlParameter("@UsarEnReporte", propiedad.UsarEnReporte ?? (object)DBNull.Value)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_Insertar_Propiedad @NombrePropiedad, @Etiqueta, @TipoCampo, @ValorDefecto, @EsRequerido, @UsarComoFiltro, @UsarEnReporte",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al insertar la propiedad.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> UpdatePropiedad(PropiedadDTO propiedad)
        {
            var parametros = new[]
            {
                new SqlParameter("@PropiedadId", propiedad.PropiedadId),
                new SqlParameter("@NombrePropiedad", propiedad.NombrePropiedad ?? (object)DBNull.Value),
                new SqlParameter("@Etiqueta", propiedad.Etiqueta ?? (object)DBNull.Value),
                new SqlParameter("@TipoCampo", propiedad.TipoCampo ?? (object)DBNull.Value),
                new SqlParameter("@ValorDefecto", propiedad.ValorDefecto ?? (object)DBNull.Value),


                new SqlParameter("@EsRequerido", (propiedad.EsRequerido ?? false) ? 1 : 0),
                new SqlParameter("@UsarComoFiltro", (propiedad.UsarComoFiltro ?? false) ? 1 : 0),
                new SqlParameter("@UsarEnReporte", (propiedad.UsarEnReporte ?? false) ? 1 : 0)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_ModificarPropiedad @PropiedadId, @NombrePropiedad, @Etiqueta, @TipoCampo, @ValorDefecto, @EsRequerido, @UsarComoFiltro, @UsarEnReporte",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al actualizar la propiedad.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
