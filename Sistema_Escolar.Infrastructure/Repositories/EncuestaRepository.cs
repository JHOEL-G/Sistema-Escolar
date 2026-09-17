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
using System.Data;
using System.Reflection.Metadata;
using System.Text;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class EncuestaRepository : IRecursoEncuestaRepository
    {
        private readonly ConfiaContext _context;

        public EncuestaRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRecursoEncuesta(RecursoEncuestaDTO dto)
        {
            var bancasTable = new DataTable();
            bancasTable.Columns.Add("Id", typeof(int));
            foreach (var b in dto.BancasIds ?? new List<BancaPreguntaSeleccionadaDTO>())
                bancasTable.Rows.Add(b.BancaId);

            var preguntasTable = new DataTable();
            preguntasTable.Columns.Add("TextoPregunta", typeof(string));
            preguntasTable.Columns.Add("TipoPregunta", typeof(string));
            preguntasTable.Columns.Add("OrdenPregunta", typeof(int));
            preguntasTable.Columns.Add("LimiteRespuestas", typeof(int));

            foreach (var p in dto.Preguntas ?? new List<EncuestaPreguntaDTO>())
                preguntasTable.Rows.Add(p.TextoPregunta, p.TipoPregunta, p.OrdenPregunta, p.LimiteRespuestas);

            var opcionesTable = new DataTable();
            opcionesTable.Columns.Add("PreguntaTexto", typeof(string));
            opcionesTable.Columns.Add("PreguntaOrden", typeof(int));
            opcionesTable.Columns.Add("TituloOpcion", typeof(string));
            opcionesTable.Columns.Add("TextoOpcion", typeof(string));
            opcionesTable.Columns.Add("OrdenOpcion", typeof(int));

            foreach (var p in dto.Preguntas ?? new List<EncuestaPreguntaDTO>())
                foreach (var o in p.Opciones ?? new List<EncuestaOpcionDTO>())
                    opcionesTable.Rows.Add(p.TextoPregunta, p.OrdenPregunta, o.TituloOpcion, o.TextoOpcion, o.OrdenOpcion);

            var parametros = new[]
            {
        new SqlParameter("@ModuloRecursoId", dto.ModuloRecursoId),
        new SqlParameter("@Instrucciones",   dto.Instrucciones ?? (object)DBNull.Value),
        new SqlParameter("@Descripcion",     dto.Descripcion   ?? (object)DBNull.Value), // ✅
        new SqlParameter("@BancasPreguntas", bancasTable)                                // ✅
        {
            SqlDbType = SqlDbType.Structured,
            TypeName  = "dbo.TipoListaIds"
        },
        new SqlParameter("@Preguntas", preguntasTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName  = "dbo.TipoEncuestaPregunta"
        },
        new SqlParameter("@Opciones", opcionesTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName  = "dbo.TipoEncuestaOpcion"
        }
    };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_CrearRecursoEncuesta @ModuloRecursoId, @Instrucciones, @Descripcion, @BancasPreguntas, @Preguntas, @Opciones",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Datos no guardados en la base de datos");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
