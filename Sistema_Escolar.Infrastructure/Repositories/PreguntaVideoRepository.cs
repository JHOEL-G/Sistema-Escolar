using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class PreguntaVideoRepository : IPreguntaVideoRepository
    {
        private readonly ConfiaContext _context;

        public PreguntaVideoRepository (ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateVideoPregunta(PreguntaVideoDTO dto)
        {
            var tablaOpciones = new DataTable();
            tablaOpciones.Columns.Add("TextoOpcion", typeof(string));
            tablaOpciones.Columns.Add("EsCorrecta", typeof(bool));

            foreach (var opt in dto.Opciones)
            {
                tablaOpciones.Rows.Add(opt.TextoOpcion, opt.EsCorrecta);
            }

            var parametros = new[]
            {
                new SqlParameter("@ModuloRecursoId", dto.ModuloRecursoId),
                new SqlParameter("@VideoPath", dto.VideoPath),
                new SqlParameter("@TextoPregunta", dto.TextoPregunta),
                new SqlParameter("@SegundoMarca", dto.SegundoMarca),
                new SqlParameter("@TipoPreguntaId", dto.TipoPreguntaId),
                new SqlParameter("@PuntosValor", dto.PuntosValor ?? 0),
                new SqlParameter("@Opciones", SqlDbType.Structured) {
                    Value = tablaOpciones,
                    TypeName = "dbo.Type_Opciones_Video"
                }
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_GuardarPreguntaConOpciones @ModuloRecursoId, @VideoPath, @TextoPregunta, @SegundoMarca, @TipoPreguntaId, @PuntosValor, @Opciones", parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Video interactivo no creado");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<PreguntaVideoDTO> GetPorId(int id)
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (var db = new SqlConnection(connectionString))
            {
                using (var multi = await db.QueryMultipleAsync("sp_ObtenerPreguntaVideoPorId",
                       new { PreguntaVideoId = id },
                       commandType: CommandType.StoredProcedure))
                {
                    var pregunta = await multi.ReadFirstOrDefaultAsync<PreguntaVideoDTO>();

                    if (pregunta != null)
                    {
                        var opciones = await multi.ReadAsync<OpcionesPreguntaDTO>();
                        pregunta.Opciones = opciones.ToList();
                    }

                    return pregunta;
                }
            }
        }

        public async Task<OperationResult> UpdatePreguntasVideo(int moduloRecursoId, List<PreguntaVideoDTO> preguntas)
        {
            var preguntasTable = new DataTable();
            preguntasTable.Columns.Add("PreguntaOrden", typeof(int));
            preguntasTable.Columns.Add("TextoPregunta", typeof(string));
            preguntasTable.Columns.Add("VideoPath", typeof(string));
            preguntasTable.Columns.Add("SegundoMarca", typeof(int));
            preguntasTable.Columns.Add("TipoPreguntaId", typeof(int));
            preguntasTable.Columns.Add("PuntosValor", typeof(decimal));
            preguntasTable.Columns.Add("Activo", typeof(bool));

            var opcionesTable = new DataTable();
            opcionesTable.Columns.Add("PreguntaOrden", typeof(int));
            opcionesTable.Columns.Add("TextoOpcion", typeof(string));
            opcionesTable.Columns.Add("EsCorrecta", typeof(bool));

            int orden = 0;
            foreach (var p in preguntas)
            {
                preguntasTable.Rows.Add(
                    orden,
                    p.TextoPregunta ?? "",
                    p.VideoPath ?? "",
                    p.SegundoMarca,
                    p.TipoPreguntaId,
                    (decimal)(p.PuntosValor ?? 0),
                    true
                );

                foreach (var o in p.Opciones ?? new List<OpcionesPreguntaDTO>())
                {
                    opcionesTable.Rows.Add(
                        orden,
                        o.TextoOpcion ?? "",
                        o.EsCorrecta
                    );
                }
                orden++;
            }

            var parametros = new[]
            {
        new SqlParameter("@ModuloRecursoId", moduloRecursoId),
        new SqlParameter("@Preguntas", preguntasTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName  = "dbo.TipoPreguntaVideoItem"
        },
        new SqlParameter("@Opciones", opcionesTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName  = "dbo.TipoOpcionPreguntaVideo"
        }
    };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_ActualizarPreguntasVideoInteractivo @ModuloRecursoId, @Preguntas, @Opciones",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("No se pudieron actualizar las preguntas.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
