using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using Sistema_Escolar_Confia.Models;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly ConfiaContext _context;

        public CursoRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateCurso(CursoDTO cursoDto)
        {
            var modulosTable = new DataTable();
            modulosTable.Columns.Add("Orden", typeof(int));
            modulosTable.Columns.Add("ModuloTitulo", typeof(string));
            modulosTable.Columns.Add("Descripcion", typeof(string));

            foreach (var mod in cursoDto.Modulos ?? new List<ModuloDTO>())
            {
                modulosTable.Rows.Add(
                    mod.OrdenModulo,
                    mod.ModuloTitulo ?? "Sin título",
                    mod.Descripcion ?? "Sin descripción"
                );
            }

            var recursosTable = new DataTable();
            recursosTable.Columns.Add("ModuloIndice", typeof(int));
            recursosTable.Columns.Add("RecursoId", typeof(int));
            recursosTable.Columns.Add("OrdenRecurso", typeof(int));
            recursosTable.Columns.Add("Titulo", typeof(string));           
            recursosTable.Columns.Add("Descripcion", typeof(string));
            recursosTable.Columns.Add("Ponderacion", typeof(int)); 

            foreach (var rec in cursoDto.Recursos ?? new List<RecursoDTO>())
            {
                var moduloOrden = cursoDto.Modulos?
                        .FirstOrDefault(m => m.ModuloId == rec.ModuloId)?.OrdenModulo ?? rec.ModuloId;

                recursosTable.Rows.Add(
                    moduloOrden,
                    rec.RecursoId,
                    rec.OrdenRecurso,
                    rec.Titulo ?? "Sin título",
                    rec.Descripcion ?? "",
                    rec.Ponderacion ?? 0
                );
            }

            var instructoresTable = new DataTable();
            instructoresTable.Columns.Add("InstructorId", typeof(int));
            instructoresTable.Columns.Add("EsPrincipal", typeof(bool));

            if (cursoDto.InstructorIds != null && cursoDto.InstructorIds.Any())
            {
                for (int i = 0; i < cursoDto.InstructorIds.Count; i++)
                {
                    instructoresTable.Rows.Add(
                        cursoDto.InstructorIds[i],
                        i == 0  
                    );
                }
            }

            var parametros = new[]
            {
        new SqlParameter("@NombreCurso", cursoDto.NombreCurso ?? (object)DBNull.Value),
        new SqlParameter("@HhabilitarFechaCurso", cursoDto.HabilitarFechaCurso),
        new SqlParameter("@ImagenPortadaPath", cursoDto.ImagenPortadaPath ?? (object)DBNull.Value),
        new SqlParameter("@DificultadId", cursoDto.DificultadId ?? (object)DBNull.Value),
        new SqlParameter("@LenguajeId", cursoDto.LenguajeId ?? (object)DBNull.Value),
        new SqlParameter("@DescripcionCurso", cursoDto.DescripcionCurso ?? (object)DBNull.Value),

        new SqlParameter("@CaracteristicasQueAprendere",
            cursoDto.CaracteristicasQueAprendere ?? (object)DBNull.Value),

        new SqlParameter("@CaracteristicasHabilidades",
            cursoDto.CaracteristicasHabilidades ?? (object)DBNull.Value),

        new SqlParameter("@CaracteristicasRequerimientos",
            cursoDto.CaracteristicasRequerimientos ?? (object)DBNull.Value),

        new SqlParameter("@VideoPromocionalPath", cursoDto.VideoPromocionalPath ?? (object)DBNull.Value),
        new SqlParameter("@Avance", cursoDto.Avance ?? (object)DBNull.Value),
        new SqlParameter("@RetroalimentacionId", cursoDto.RetroalimentacionId ?? (object)DBNull.Value),
        new SqlParameter("@DuracionCurso", cursoDto.DuracionCurso ?? (object)DBNull.Value),
        new SqlParameter("@MensajeBienvenida", cursoDto.MensajeBienvenida ?? (object)DBNull.Value),
        new SqlParameter("@InstructorId", cursoDto.InstructorId ?? (object)DBNull.Value),
        new SqlParameter("@Reacreditacion", cursoDto.Reacreditacion),
        new SqlParameter("@EstaPublicado", cursoDto.EstaPublicado),
         new SqlParameter("@PorQueInscribirmeCurso", cursoDto.PorQueInscribirmeCurso ?? (object)DBNull.Value),
new SqlParameter("@Calificacion",           cursoDto.Calificacion           ?? (object)DBNull.Value),
new SqlParameter("@CursoReacreditacionId",  cursoDto.CursoReacreditacionId  ?? (object)DBNull.Value),
new SqlParameter("@PeriodoVigencia",        cursoDto.PeriodoVigencia        ?? (object)DBNull.Value),
        new SqlParameter("@Modulos", modulosTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.TipoModulo"

        },
        new SqlParameter("@Recursos", recursosTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.TipoRecurso"
        },
        new SqlParameter("@Instructores", instructoresTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.TipoInstructor"
        }
    };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
               "EXEC sp_CrearCursoCompleto " +
"@NombreCurso, @HhabilitarFechaCurso, @ImagenPortadaPath, @DificultadId, @LenguajeId, " +
"@DescripcionCurso, @CaracteristicasQueAprendere, @CaracteristicasHabilidades, @CaracteristicasRequerimientos, " +
"@VideoPromocionalPath, @Avance, @RetroalimentacionId, @DuracionCurso, @MensajeBienvenida, " +
"@InstructorId, @Reacreditacion, @EstaPublicado, " +
"@PorQueInscribirmeCurso, @Calificacion, @CursoReacreditacionId, @PeriodoVigencia, " +
"@Modulos, @Recursos, @Instructores",
                parametros
            ).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al crear el curso.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje ?? "Curso creado correctamente")
                : OperationResult.Fail(sp.Mensaje ?? "No se pudo crear el curso");
        }


        public async Task<CursoDTO?> GetCursoById(int id)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            using var multi = await(connection as SqlConnection)!.QueryMultipleAsync(
                "sp_ObtenerCursoPorId",
                new { CursoId = id },
                commandType: CommandType.StoredProcedure
            );

            var curso = await multi.ReadFirstOrDefaultAsync<CursoDTO>();

            if (curso != null)
            {
                curso.Modulos = (await multi.ReadAsync<ModuloDTO>()).ToList();
                var recursos = (await multi.ReadAsync<RecursoDetalleDTO>()).ToList();

                var instructores = (await multi.ReadAsync<dynamic>()).ToList();
                curso.InstructorIds = instructores.Select(i => (int)i.InstructorId).ToList();

                curso.Recursos = recursos.Select(r =>
                {
                    var recurso = new RecursoDTO
                    {
                        ModuloId = r.ModuloId,
                        RecursoId = r.RecursoId,
                        OrdenRecurso = r.OrdenRecurso,
                        Titulo = r.TituloRecurso,
                        Descripcion = r.TituloRecurso,
                        ModuloRecursoId = r.ModuloRecursoId,
                        NombreTipo = r.NombreTipo,   
                        Icono = r.Icono,
                        Ponderacion = r.Ponderacion 
                    };

                    if (!string.IsNullOrEmpty(r.DatosDetalle))
                    {
                        try
                        {
                            var datosJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(r.DatosDetalle);
                            var dataJson = new Dictionary<string, object?>();

                            switch (r.RecursoId)
                            {
                                case 1: 
                                    dataJson["evaluacionId"] = ValoresJson.GetIntOrNull(datosJson, "EvaluacionId");
                                    dataJson["instrucciones"] = ValoresJson.GetStringOrNull(datosJson, "evaluacionInstrucciones");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "evaluacionDescripcion");
                                    dataJson["agregarPonderacion"] = ValoresJson.GetBoolOrNull(datosJson, "evaluacionPonderacion");
                                    dataJson["preguntasAleatorias"] = ValoresJson.GetBoolOrNull(datosJson, "PreguntasAleatorias");
                                    dataJson["oportunidades"] = ValoresJson.GetIntOrNull(datosJson, "Oportunidades");
                                    dataJson["permitirReinicio"] = ValoresJson.GetStringOrNull(datosJson, "PermitirReinicio");
                                    dataJson["preguntasCorrectasAprobar"] = ValoresJson.GetIntOrNull(datosJson, "PreguntasCorrectasAprobar");
                                    dataJson["tiempoHoras"] = ValoresJson.GetIntOrNull(datosJson, "TiempoHoras");
                                    dataJson["tiempoMinutos"] = ValoresJson.GetIntOrNull(datosJson, "TiempoMinutos");
                                    dataJson["tipoCalificacionId"] = ValoresJson.GetIntOrNull(datosJson, "TipoCalificacionId");
                                    dataJson["bancasIds"] = datosJson.ContainsKey("BancasPreguntas") && datosJson["BancasPreguntas"].ValueKind != JsonValueKind.Null
                                        ? datosJson["BancasPreguntas"] : (object)new List<object>();
                                    dataJson["preguntas"] = datosJson.ContainsKey("Preguntas") &&
                                            datosJson["Preguntas"].ValueKind != JsonValueKind.Null
                                            ? datosJson["Preguntas"]  
                                            : (object)new List<object>();
                                    break;

                                case 2: 
                                    dataJson["foroId"] = ValoresJson.GetIntOrNull(datosJson, "ForoId");
                                    dataJson["nombreForo"] = ValoresJson.GetStringOrNull(datosJson, "NombreForo");
                                    dataJson["instrucciones"] = ValoresJson.GetStringOrNull(datosJson, "foroInstrucciones");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "foroDescripcion");
                                    dataJson["agregarPonderacion"] = ValoresJson.GetBoolOrNull(datosJson, "foroPonderacion");
                                    dataJson["privacidad"] = ValoresJson.GetStringOrNull(datosJson, "foroPrivacidad");
                                    dataJson["archivos"] = datosJson.ContainsKey("foroArchivos") && datosJson["foroArchivos"].ValueKind != JsonValueKind.Null ? datosJson["foroArchivos"] : (object)new List<object>();
                                    break;

                                case 3: 
                                    dataJson["tareaId"] = ValoresJson.GetIntOrNull(datosJson, "TareaId");
                                    dataJson["instrucciones"] = ValoresJson.GetStringOrNull(datosJson, "tareaInstrucciones");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "tareaDescripcion");
                                    dataJson["agregarPonderacion"] = ValoresJson.GetBoolOrNull(datosJson, "tareaPonderacion");
                                    dataJson["privacidad"] = ValoresJson.GetStringOrNull(datosJson, "tareaPrivacidad");
                                    dataJson["archivos"] = datosJson.ContainsKey("tareaArchivos") && datosJson["tareaArchivos"].ValueKind != JsonValueKind.Null ? datosJson["tareaArchivos"] : (object)new List<object>();
                                    break;

                                case 4: 
                                    dataJson["videoId"] = ValoresJson.GetIntOrNull(datosJson, "VideoId");
                                    dataJson["videoPath"] = ValoresJson.GetStringOrNull(datosJson, "VideoPath");
                                    dataJson["videoLink"] = ValoresJson.GetStringOrNull(datosJson, "VideoLink");
                                    dataJson["tipoSubida"] = ValoresJson.GetStringOrNull(datosJson, "tipoVideo");
                                    dataJson["hacerVisibleDashboard"] = ValoresJson.GetBoolOrNull(datosJson, "videoVisibleDashboard");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "videoDescripcion");  
                                    break;

                                case 5:
                                    dataJson["lecturaId"] = ValoresJson.GetIntOrNull(datosJson, "LecturaId");
                                    dataJson["tipoLecturaId"] = ValoresJson.GetIntOrNull(datosJson, "TipoLecturaId");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "lecturaDescripcion");
                                    dataJson["contenidoHTML"] = ValoresJson.GetStringOrNull(datosJson, "ContenidoHTML");
                                    dataJson["archivoPDFPath"] = ValoresJson.GetStringOrNull(datosJson, "ArchivoPDFPath");
                                    dataJson["nombreArchivoPDF"] = ValoresJson.GetStringOrNull(datosJson, "NombreArchivoPDF");
                                    dataJson["hacerVisibleDashboard"] = ValoresJson.GetBoolOrNull(datosJson, "lecturaVisibleDashboard");
                                    dataJson["archivosAdjuntos"] = datosJson.ContainsKey("ArchivosAdjuntos")
                                        && datosJson["ArchivosAdjuntos"].ValueKind != JsonValueKind.Null
                                        ? datosJson["ArchivosAdjuntos"]
                                        : (object)new List<object>();
                                    break;

                                case 6: 
                                    dataJson["zoomId"] = ValoresJson.GetIntOrNull(datosJson, "ZoomId");
                                    dataJson["enlaceZoom"] = ValoresJson.GetStringOrNull(datosJson, "EnlaceZoom");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "zoomDescripcion");
                                    break;

                                case 7: 
                                    dataJson["embebidoId"] = ValoresJson.GetIntOrNull(datosJson, "EmbebidoId");
                                    dataJson["enlaceEmbebido"] = ValoresJson.GetStringOrNull(datosJson, "EnlaceEmbebido");
                                    break;

                                case 8:
                                    dataJson["ScormId"] = ValoresJson.GetIntOrNull(datosJson, "ScormId");
                                    dataJson["ArchivoPath"] = ValoresJson.GetStringOrNull(datosJson, "scormArchivoPath");
                                    dataJson["NombreArchivo"] = ValoresJson.GetStringOrNull(datosJson, "scormNombreArchivo");
                                    dataJson["TamañoMB"] = ValoresJson.GetDecimalOrNull(datosJson, "scormTamañoMB");
                                    dataJson["AgregarPonderacion"] = ValoresJson.GetBoolOrNull(datosJson, "scormPonderacion");
                                    dataJson["PermitirModoPantallaCompleta"] = ValoresJson.GetBoolOrNull(datosJson, "PermitirModoPantallaCompleta");
                                    dataJson["TipoCalificacionId"] = ValoresJson.GetIntOrNull(datosJson, "scormTipoCalificacionId");  
                                    dataJson["Descripcion"] = ValoresJson.GetStringOrNull(datosJson, "scormDescripcion");             
                                    break;

                                case 9: 
                                    dataJson["encuestaId"] = ValoresJson.GetIntOrNull(datosJson, "EncuestaId");
                                    dataJson["instrucciones"] = ValoresJson.GetStringOrNull(datosJson, "encuestaInstrucciones");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "encuestaDescripcion");
                                    dataJson["bancasIds"] = datosJson.ContainsKey("EncuestaBancas") && datosJson["EncuestaBancas"].ValueKind != JsonValueKind.Null
                                        ? datosJson["EncuestaBancas"] : (object)new List<object>();
                                    dataJson["preguntas"] = datosJson.ContainsKey("EncuestaPreguntas")
                                                                 && datosJson["EncuestaPreguntas"].ValueKind != JsonValueKind.Null
                                                                     ? datosJson["EncuestaPreguntas"]
                                                                     : (object)new List<object>();
                                    break;

                                case 10:
                                    dataJson["sesionPresencialId"] = ValoresJson.GetIntOrNull(datosJson, "SesionPresencialId");
                                    dataJson["fechaSesion"] = ValoresJson.GetDateTimeOrNull(datosJson, "FechaSesion");
                                    dataJson["lugar"] = ValoresJson.GetStringOrNull(datosJson, "Lugar");
                                    dataJson["duracion"] = ValoresJson.GetIntOrNull(datosJson, "sesionDuracion");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "sesionDescripcion");   
                                    dataJson["direccion"] = ValoresJson.GetStringOrNull(datosJson, "sesionDireccion");     
                                    dataJson["instrucciones"] = ValoresJson.GetStringOrNull(datosJson, "sesionInstrucciones"); 
                                    dataJson["horaFin"] = ValoresJson.GetStringOrNull(datosJson, "sesionHoraFin");       
                                    break;

                                case 11:
                                    dataJson["evaluacionPresencialId"] = ValoresJson.GetIntOrNull(datosJson, "EvaluacionPresencialId");
                                    dataJson["agregarPonderacion"] = ValoresJson.GetBoolOrNull(datosJson, "evaluacionPresencialPonderacion");
                                    dataJson["colaboradorSolicitarRevision"] = ValoresJson.GetBoolOrNull(datosJson, "ColaboradorSolicitarRevision");
                                    dataJson["tipoCalificacionId"] = ValoresJson.GetIntOrNull(datosJson, "evaluacionPresencialTipoCalificacion");
                                    dataJson["descripcion"] = ValoresJson.GetStringOrNull(datosJson, "evaluacionPresencialDescripcion");
                                    dataJson["rubricas"] = datosJson.ContainsKey("Rubricas") && datosJson["Rubricas"].ValueKind != JsonValueKind.Null
                                        ? datosJson["Rubricas"] : (object)new List<object>();
                                    break;

                                case 12: 
                                    dataJson["videoPath"] = ValoresJson.GetStringOrNull(datosJson, "VideoPath");
                                    dataJson["videoLink"] = ValoresJson.GetStringOrNull(datosJson, "VideoLink");
                                    dataJson["tipoSubida"] = ValoresJson.GetStringOrNull(datosJson, "tipoVideo");
                                    dataJson["hacerVisibleDashboard"] = ValoresJson.GetBoolOrNull(datosJson, "videoVisibleDashboard");
                                    dataJson["preguntas"] = datosJson.ContainsKey("PreguntasInteractivas") && datosJson["PreguntasInteractivas"].ValueKind != JsonValueKind.Null
                                        ? datosJson["PreguntasInteractivas"] : (object)new List<object>();
                                    break;
                            }

                            var jsonString = JsonSerializer.Serialize(dataJson);
                            recurso.DataJson = JsonSerializer.Deserialize<JsonElement>(jsonString);
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"Error deserializando DatosDetalle: {ex.Message}");
                        }
                    }

                    return recurso;
                }).ToList();
            }

            return curso;
        }


        public async Task<IEnumerable<ListarCursoDTO>> GetCursos(bool soloActivos = true)
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            var cursos = await (connection as SqlConnection)!.QueryAsync<ListarCursoDTO>(
                "sp_ListarCursos",
                new
                {
                    InstructorId = (int?)null,
                    DificultadId = (int?)null,
                    LenguajeId = (int?)null,
                    SoloActivos = soloActivos
                },
                commandType: CommandType.StoredProcedure
            );

            var lista = cursos.ToList();

            foreach (var curso in lista)
            {
                if (!string.IsNullOrEmpty(curso.Modulos))
                    curso.ModulosParsed = JsonSerializer.Deserialize<List<ModuloDTO>>(curso.Modulos,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!string.IsNullOrEmpty(curso.Instructores))
                    curso.InstructoresParsed = JsonSerializer.Deserialize<List<InstructorCursoDTO>>(curso.Instructores,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return lista;
        }

        public async Task<OperationResult> UpdateCurso(CursoDTO cursoDto)
        {
            var modulosTable = new DataTable();
            modulosTable.Columns.Add("Orden", typeof(int));
            modulosTable.Columns.Add("ModuloTitulo", typeof(string));
            modulosTable.Columns.Add("Descripcion", typeof(string));

            foreach (var mod in cursoDto.Modulos ?? new List<ModuloDTO>())
            {
                modulosTable.Rows.Add(
                    mod.OrdenModulo,
                    mod.ModuloTitulo ?? "Sin título",
                    mod.Descripcion ?? "Sin descripción"
                );
            }

            var recursosTable = new DataTable();
            recursosTable.Columns.Add("ModuloIndice", typeof(int));
            recursosTable.Columns.Add("RecursoId", typeof(int));
            recursosTable.Columns.Add("OrdenRecurso", typeof(int));
            recursosTable.Columns.Add("Titulo", typeof(string));       
            recursosTable.Columns.Add("Descripcion", typeof(string));
            recursosTable.Columns.Add("Ponderacion", typeof(int));  

            foreach (var rec in cursoDto.Recursos ?? new List<RecursoDTO>())
            {
                var moduloOrden = cursoDto.Modulos?
                    .FirstOrDefault(m => m.ModuloId == rec.ModuloId)?.OrdenModulo ?? rec.ModuloId;

                recursosTable.Rows.Add(
                    moduloOrden,       
                    rec.RecursoId,
                    rec.OrdenRecurso,
                    rec.Titulo ?? "Sin título",
                    rec.Descripcion ?? "",
                    rec.Ponderacion ?? 0 
                );
            }

            var instructoresTable = new DataTable();
            instructoresTable.Columns.Add("InstructorId", typeof(int));
            instructoresTable.Columns.Add("EsPrincipal", typeof(bool));

            if (cursoDto.InstructorIds != null && cursoDto.InstructorIds.Any())
            {
                for (int i = 0; i < cursoDto.InstructorIds.Count; i++)
                {
                    instructoresTable.Rows.Add(
                        cursoDto.InstructorIds[i],
                        i == 0 
                    );
                }
            }

            var parametros = new[]
            {
                new SqlParameter("@CursoId", cursoDto.CursoId),
                new SqlParameter("@NombreCurso", cursoDto.NombreCurso ?? (object)DBNull.Value),
                new SqlParameter("@HhabilitarFechaCurso", cursoDto.HabilitarFechaCurso),
                new SqlParameter("@ImagenPortadaPath", cursoDto.ImagenPortadaPath ?? (object)DBNull.Value),
                new SqlParameter("@DificultadId", cursoDto.DificultadId ?? (object)DBNull.Value),
                new SqlParameter("@LenguajeId", cursoDto.LenguajeId ?? (object)DBNull.Value),
                new SqlParameter("@DescripcionCurso", cursoDto.DescripcionCurso ?? (object)DBNull.Value),
                new SqlParameter("@CaracteristicasQueAprendere", cursoDto.CaracteristicasQueAprendere ?? (object)DBNull.Value),
                new SqlParameter("@CaracteristicasHabilidades", cursoDto.CaracteristicasHabilidades ?? (object)DBNull.Value),
                new SqlParameter("@CaracteristicasRequerimientos", cursoDto.CaracteristicasRequerimientos ?? (object)DBNull.Value),
                new SqlParameter("@VideoPromocionalPath", cursoDto.VideoPromocionalPath ?? (object)DBNull.Value),
                new SqlParameter("@Avance", cursoDto.Avance ?? (object)DBNull.Value),
                new SqlParameter("@RetroalimentacionId", cursoDto.RetroalimentacionId ?? (object)DBNull.Value),
                new SqlParameter("@DuracionCurso", cursoDto.DuracionCurso ?? (object)DBNull.Value),
                new SqlParameter("@MensajeBienvenida", cursoDto.MensajeBienvenida ?? (object)DBNull.Value),
                new SqlParameter("@InstructorId", cursoDto.InstructorId ?? (object)DBNull.Value),
                new SqlParameter("@Reacreditacion", cursoDto.Reacreditacion),
                new SqlParameter("@EstaPublicado", cursoDto.EstaPublicado),
                 new SqlParameter("@PorQueInscribirmeCurso", cursoDto.PorQueInscribirmeCurso ?? (object)DBNull.Value),
new SqlParameter("@Calificacion",           cursoDto.Calificacion           ?? (object)DBNull.Value),
new SqlParameter("@CursoReacreditacionId",  cursoDto.CursoReacreditacionId  ?? (object)DBNull.Value),
new SqlParameter("@PeriodoVigencia",        cursoDto.PeriodoVigencia        ?? (object)DBNull.Value),
                new SqlParameter("@Modulos", modulosTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.TipoModulo"
                },
                new SqlParameter("@Instructores", instructoresTable)
{
    SqlDbType = SqlDbType.Structured,
    TypeName = "dbo.TipoInstructor"
},
                new SqlParameter("@Recursos", recursosTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.TipoRecurso"
                }
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
              "EXEC sp_ModificarCursoCompleto " +
"@CursoId, @NombreCurso, @HhabilitarFechaCurso, @ImagenPortadaPath, @DificultadId, @LenguajeId, " +
"@DescripcionCurso, @CaracteristicasQueAprendere, @CaracteristicasHabilidades, @CaracteristicasRequerimientos, " +
"@VideoPromocionalPath, @Avance, @RetroalimentacionId, @DuracionCurso, @MensajeBienvenida, " +
"@InstructorId, @Reacreditacion, @EstaPublicado, " +
"@PorQueInscribirmeCurso, @Calificacion, @CursoReacreditacionId, @PeriodoVigencia, " +
"@Modulos, @Instructores, @Recursos",
                parametros
            ).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al actualizar el curso.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje ?? "Curso actualizado correctamente")
                : OperationResult.Fail(sp.Mensaje ?? "No se pudo actualizar el curso");
        }

        public async Task<int> ObtenerIdRelacion(int cursoId, int moduloOrden, int recursoOrden)
        {
            var connection = _context.Database.GetDbConnection();
            const string sql = @"
                SELECT mr.ModuloRecursoId 
                FROM MODULO_RECURSO mr
                JOIN MODULOS m ON mr.ModuloId = m.ModuloId
                WHERE m.CursoId = @cursoId 
                  AND m.OrdenModulo = @moduloOrden 
                  AND mr.OrdenRecurso = @recursoOrden";

            return await connection.QueryFirstOrDefaultAsync<int>(sql, new { cursoId, moduloOrden, recursoOrden });
        }

        public async Task<OperationResult> DuplicarCurso(int cursoId)
        {
            var parametros = new[]
                {
                    new SqlParameter("@CursoId", cursoId)
                };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_DuplicarCurso @CursoId",
                parametros
            ).ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null) return OperationResult.Fail("Error al duplicar el curso.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje ?? "Curso duplicado correctamente")
                : OperationResult.Fail(sp.Mensaje ?? "No se pudo duplicar el curso");
        }

        public async Task<List<RecursoIdCreadoDTO>> AgregarRelacionesModuloRecurso(int cursoId, List<ModuloDTO> modulos, List<RecursoDTO> recursos)
        {
            var modulosTable = new DataTable();
            modulosTable.Columns.Add("Orden", typeof(int));
            modulosTable.Columns.Add("ModuloTitulo", typeof(string));
            modulosTable.Columns.Add("Descripcion", typeof(string));

            foreach (var mod in modulos)
                modulosTable.Rows.Add(mod.OrdenModulo, mod.ModuloTitulo ?? "Sin título", mod.Descripcion ?? "");

            var recursosTable = new DataTable();
            recursosTable.Columns.Add("ModuloIndice", typeof(int));
            recursosTable.Columns.Add("RecursoId", typeof(int));
            recursosTable.Columns.Add("OrdenRecurso", typeof(int));
            recursosTable.Columns.Add("Titulo", typeof(string));
            recursosTable.Columns.Add("Descripcion", typeof(string));

            foreach (var rec in recursos)
            {
                var moduloOrden = modulos
                    .FirstOrDefault(m => m.ModuloId == rec.ModuloId)?.OrdenModulo ?? rec.ModuloId;

                recursosTable.Rows.Add(
                    moduloOrden,           
                    rec.RecursoId,
                    rec.OrdenRecurso,
                    rec.Titulo ?? "Sin título",
                    rec.Descripcion ?? ""
                );
            }

            var parametros = new[]
            {
        new SqlParameter("@CursoId", cursoId),
        new SqlParameter("@Modulos", modulosTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.TipoModulo"
        },
        new SqlParameter("@Recursos", recursosTable)
        {
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.TipoRecurso"
        }
    };

            return await _context.Database.SqlQueryRaw<RecursoIdCreadoDTO>(
                "EXEC sp_AgregarRecursosCurso @CursoId, @Modulos, @Recursos",
                parametros
            ).ToListAsync();
        }

        public async Task ActualizarPonderacionRecursos(List<PonderacionItemDTO> ponderaciones)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            foreach (var item in ponderaciones)
            {
                await connection.ExecuteAsync(
                    "UPDATE MODULO_RECURSO SET Ponderacion = @Ponderacion WHERE ModuloRecursoId = @ModuloRecursoId",
                    new { item.Ponderacion, item.ModuloRecursoId });
            }
        }

        public async Task ActualizarCriterioCurso(int cursoId, decimal? calificacion, int? requisitoAvance)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await connection.ExecuteAsync(@"
        UPDATE CURSO
        SET Calificacion    = COALESCE(@Calificacion, Calificacion),
            RequisitoAvance = COALESCE(@RequisitoAvance, RequisitoAvance)
        WHERE CursoId = @CursoId",
                new { CursoId = cursoId, Calificacion = calificacion, RequisitoAvance = requisitoAvance });
        }

        public async Task<OperationResult> CambiarEstadoCurso(int cursoId, int adminId, string accion, string? motivo = null)
        {
            var parametros = new[]
            {
                new SqlParameter("@CursoId", cursoId),
                new SqlParameter("@AdminId", adminId),
                new SqlParameter("@Accion", accion),
                new SqlParameter("@Motivo", motivo ?? (object)DBNull.Value)
            };

            var curso = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_CambiarEstadoCurso @CursoId, @AdminId, @Accion, @Motivo", parametros).ToListAsync();

            var sp = curso.FirstOrDefault();

            if (sp == null)
                return OperationResult.Fail("No se pudo cambiar el estado del curso.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje ?? "Estado del curso cambiado correctamente")
                : OperationResult.Fail(sp.Mensaje ?? "No se pudo cambiar el estado del curso");
        }

        public async Task<OperationResult> EliminarCurso(int cursoId, int adminId, string? motivo = null)
        {
            var parametros = new[]
            {
                new SqlParameter("@CursoId", cursoId),
                new SqlParameter("@AdminId", adminId),
                new SqlParameter("@Motivo", motivo ?? (object)DBNull.Value)
            };

            var curso = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_EliminarCursoLogico @CursoId, @AdminId, @Motivo", parametros).ToListAsync();

            var sp = curso.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al eliminar el curso.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.ResultId, sp.Mensaje ?? "Curso eliminado correctamente")
                : OperationResult.Fail(sp.Mensaje ?? "No se pudo eliminar el curso");
        }
    }
}