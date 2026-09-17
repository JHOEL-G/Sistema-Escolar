using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using Sistema_Escolar_Confia.Models;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class CrearPreguntaRepository : ICrearPreguntaRepository
    {
        private readonly ConfiaContext _context;

        public CrearPreguntaRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreatePregunta(BancaPreguntaDTO dto)
        {
            try
            {
                var dtPreguntas = new DataTable();
                dtPreguntas.Columns.Add("TipoPreguntaId", typeof(int));
                dtPreguntas.Columns.Add("TextoPregunta", typeof(string));
                dtPreguntas.Columns.Add("Explicacion", typeof(string));
                dtPreguntas.Columns.Add("PuntosValor", typeof(decimal));
                dtPreguntas.Columns.Add("Orden", typeof(int));

                var dtOpciones = new DataTable();
                dtOpciones.Columns.Add("PreguntaTextoReferencia", typeof(string));
                dtOpciones.Columns.Add("TextoOpcion", typeof(string));
                dtOpciones.Columns.Add("ExplicacionORelacion", typeof(string));
                dtOpciones.Columns.Add("EsCorrecta", typeof(bool));
                dtOpciones.Columns.Add("Orden", typeof(int));

                foreach (var p in dto.Preguntas)
                {
                    dtPreguntas.Rows.Add(p.TipoPreguntaId, p.TextoPregunta, p.Explicacion, p.PuntosValor, p.Orden);

                    foreach (var o in p.Opciones)
                    {
                        dtOpciones.Rows.Add(
                            p.TextoPregunta, 
                            o.TextoOpcion,
                            o.ExplicacionORelacion,
                            o.EsCorrecta,
                            o.Orden
                        );
                    }
                }

                var parametros = new[]
                {
                    new SqlParameter("@NombreBanca", dto.NombreBanca),
                    new SqlParameter("@Descripcion", (object)dto.Descripcion ?? DBNull.Value),
                    new SqlParameter("@ArchivoPlantilla", (object)dto.ArchivoPlantilla ?? DBNull.Value),
                    new SqlParameter("@Preguntas", dtPreguntas) { TypeName = "dbo.Type_Pregunta", SqlDbType = SqlDbType.Structured },
                    new SqlParameter("@Opciones", dtOpciones) { TypeName = "dbo.Type_Opcion", SqlDbType = SqlDbType.Structured }
                };

                var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                    "EXEC sp_Crear_Preguntas @NombreBanca, @Descripcion, @ArchivoPlantilla, @Preguntas, @Opciones", parametros).ToListAsync();

                var sp = resultado.FirstOrDefault();

                if (sp == null) return OperationResult.Fail("Las preguntas no se an guardado");

                return sp.ResultId > 0
                    ? OperationResult.Ok(sp.Mensaje)
                    : OperationResult.Fail(sp.Mensaje);
            }
            catch (Exception ex) 
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public async Task<IEnumerable<BancaPreguntaDTO>> GetPregunta()
        {
            try
            {
                var fragmentosJson = await _context.Database
                    .SqlQueryRaw<string>("EXEC sp_ObtenerTodo_Preguntas")
                    .ToListAsync();

                var jsonCompleto = string.Join("", fragmentosJson);

                if (string.IsNullOrWhiteSpace(jsonCompleto))
                    return new List<BancaPreguntaDTO>();

                return JsonSerializer.Deserialize<List<BancaPreguntaDTO>>(jsonCompleto, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<BancaPreguntaDTO>();
            }
            catch (Exception)
            {
                return new List<BancaPreguntaDTO>();
            }
        }

        public async Task<BancaPreguntaDTO?> GetPreguntaById(int bancaId)
        {
            try
            {
                var fragmentosJson = await _context.Database
                    .SqlQueryRaw<string>("EXEC sp_ObtenerPreguntasPorId @BancaId",
                        new SqlParameter("@BancaId", bancaId))
                    .ToListAsync();

                var jsonCompleto = string.Join("", fragmentosJson);

                if (string.IsNullOrWhiteSpace(jsonCompleto))
                    return null;

                var lista = JsonSerializer.Deserialize<List<BancaPreguntaDTO>>(jsonCompleto, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return lista?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> UpdatePregunta(int bancaId, BancaPreguntaDTO dto)
        {
            try
            {
                var dtPreguntas = new DataTable();
                dtPreguntas.Columns.Add("TipoPreguntaId", typeof(int));
                dtPreguntas.Columns.Add("TextoPregunta", typeof(string));
                dtPreguntas.Columns.Add("Explicacion", typeof(string));
                dtPreguntas.Columns.Add("PuntosValor", typeof(decimal));
                dtPreguntas.Columns.Add("Orden", typeof(int));

                var dtOpciones = new DataTable();
                dtOpciones.Columns.Add("PreguntaTextoReferencia", typeof(string));
                dtOpciones.Columns.Add("TextoOpcion", typeof(string));
                dtOpciones.Columns.Add("ExplicacionORelacion", typeof(string));
                dtOpciones.Columns.Add("EsCorrecta", typeof(bool));
                dtOpciones.Columns.Add("Orden", typeof(int));

                foreach (var p in dto.Preguntas)
                {
                    dtPreguntas.Rows.Add(p.TipoPreguntaId, p.TextoPregunta, p.Explicacion, p.PuntosValor, p.Orden);
                    foreach (var o in p.Opciones)
                        dtOpciones.Rows.Add(p.TextoPregunta, o.TextoOpcion, o.ExplicacionORelacion, o.EsCorrecta, o.Orden);
                }

                var parametros = new[]
                {
                    new SqlParameter("@BancaId",          bancaId),
                    new SqlParameter("@NombreBanca",      dto.NombreBanca),
                    new SqlParameter("@Descripcion",      (object?)dto.Descripcion      ?? DBNull.Value),
                    new SqlParameter("@ArchivoPlantilla", (object?)dto.ArchivoPlantilla ?? DBNull.Value),
                    new SqlParameter("@Preguntas", dtPreguntas) { TypeName = "dbo.Type_Pregunta", SqlDbType = SqlDbType.Structured },
                    new SqlParameter("@Opciones",  dtOpciones)  { TypeName = "dbo.Type_Opcion",   SqlDbType = SqlDbType.Structured }
                };

                var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                    "EXEC sp_Modificar_Preguntas @BancaId, @NombreBanca, @Descripcion, @ArchivoPlantilla, @Preguntas, @Opciones",
                    parametros).ToListAsync();

                var sp = resultado.FirstOrDefault();
                if (sp == null) return OperationResult.Fail("No se pudo actualizar el banco de preguntas");

                return sp.ResultId > 0
                    ? OperationResult.Ok(sp.Mensaje)
                    : OperationResult.Fail(sp.Mensaje);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }
    }
}
