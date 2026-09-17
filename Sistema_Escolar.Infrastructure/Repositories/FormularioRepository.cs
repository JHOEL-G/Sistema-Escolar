using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class FormularioRepository : IFormularioRepository
    {
        private readonly ConfiaContext _context;

        public FormularioRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<FormularioResultDTO>> CrearFormulario(CrearFormularioDTO dto)
        {
            try
            {
                var (dtPreguntas, dtOpciones) = BuildTables(dto.Preguntas);

                var parametros = new SqlParameter[]
                {
                new SqlParameter("@Titulo",      dto.Titulo),
                new SqlParameter("@Descripcion", (object?)dto.Descripcion ?? DBNull.Value),
                new SqlParameter("@Preguntas", dtPreguntas)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoPreguntaFormulario"
                },
                new SqlParameter("@Opciones", dtOpciones)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoOpcionFormulario"
                }
                };

                var resultado = await _context.Database
                    .SqlQueryRaw<FormularioResultDTO>(
                        "EXEC sp_CrearFormularioPlantilla @Titulo, @Descripcion, @Preguntas, @Opciones",
                        parametros)
                    .ToListAsync();

                var sp = resultado.FirstOrDefault();
                if (sp == null)
                    return OperationResult<FormularioResultDTO>.Fail("No se recibió respuesta del procedimiento.");

                return sp.ResultId > 0
                    ? OperationResult<FormularioResultDTO>.Ok(sp, sp.Mensaje)
                    : OperationResult<FormularioResultDTO>.Fail(sp.Mensaje);
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioResultDTO>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<FormularioResultDTO>> GuardarRespuestas(EnvioFormularioDTO dto)
        {
            try
            {
                var dtRespuestas = new DataTable();
                dtRespuestas.Columns.Add("PreguntaId", typeof(int));
                dtRespuestas.Columns.Add("TextoRespuesta", typeof(string));
                dtRespuestas.Columns.Add("OpcionId", typeof(int));

                foreach (var r in dto.Respuestas)
                    dtRespuestas.Rows.Add(r.PreguntaId, (object?)r.TextoRespuesta ?? DBNull.Value, (object?)r.OpcionId ?? DBNull.Value);

                var parametros = new SqlParameter[]
                {
            new SqlParameter("@PlantillaId", dto.PlantillaId),
            new SqlParameter("@UsuarioId",   (object?)dto.UsuarioId ?? DBNull.Value),
            new SqlParameter("@Respuestas",  dtRespuestas) { SqlDbType = SqlDbType.Structured, TypeName = "dbo.TipoRespuestaFormulario" }
                };

                var resultado = await _context.Database
                    .SqlQueryRaw<FormularioResultDTO>("EXEC sp_GuardarRespuestasFormulario @PlantillaId, @UsuarioId, @Respuestas", parametros)
                    .ToListAsync();

                var sp = resultado.FirstOrDefault();
                return sp?.ResultId > 0
                    ? OperationResult<FormularioResultDTO>.Ok(sp, sp.Mensaje)
                    : OperationResult<FormularioResultDTO>.Fail(sp?.Mensaje ?? "Error");
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioResultDTO>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<List<FormularioListaDTO>>> ListarFormularios()
        {
            try
            {
                var resultado = await _context.Database
                    .SqlQueryRaw<FormularioListaDTO>("EXEC sp_ListarFormularioPlantillas")
                    .ToListAsync();

                return OperationResult<List<FormularioListaDTO>>.Ok(resultado, "OK");
            }
            catch (Exception ex)
            {
                return OperationResult<List<FormularioListaDTO>>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<List<FormularioListaDTO>>> ListarFormulariosConRespuestas()
        {
            try
            {
                var resultado = await _context.Database
                    .SqlQueryRaw<FormularioListaDTO>("EXEC sp_ListarFormulariosConRespuestas")
                    .ToListAsync();

                return OperationResult<List<FormularioListaDTO>>.Ok(resultado, "OK");
            }
            catch (Exception ex)
            {
                return OperationResult<List<FormularioListaDTO>>.Fail(ex.Message);
            }

        }

        public async Task<OperationResult<FormularioResultDTO>> ModificarFormulario(int plantillaId, CrearFormularioDTO dto)
        {
            try
            {
                var (dtPreguntas, dtOpciones) = BuildTables(dto.Preguntas);

                var parametros = new SqlParameter[]
                {
                new SqlParameter("@PlantillaId", plantillaId),
                new SqlParameter("@Titulo",      dto.Titulo),
                new SqlParameter("@Descripcion", (object?)dto.Descripcion ?? DBNull.Value),
                new SqlParameter("@Preguntas", dtPreguntas)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoPreguntaFormulario"
                },
                new SqlParameter("@Opciones", dtOpciones)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName  = "dbo.TipoOpcionFormulario"
                }
                };

                var resultado = await _context.Database
                    .SqlQueryRaw<FormularioResultDTO>(
                        "EXEC sp_ModificarFormularioPlantilla @PlantillaId, @Titulo, @Descripcion, @Preguntas, @Opciones",
                        parametros)
                    .ToListAsync();

                var sp = resultado.FirstOrDefault();
                if (sp == null)
                    return OperationResult<FormularioResultDTO>.Fail("No se recibió respuesta del procedimiento.");

                return sp.ResultId > 0
                    ? OperationResult<FormularioResultDTO>.Ok(sp, sp.Mensaje)
                    : OperationResult<FormularioResultDTO>.Fail(sp.Mensaje);
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioResultDTO>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<FormularioDetalleDTO?>> ObtenerFormularioPorId(int plantillaId)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
            new SqlParameter("@PlantillaId", plantillaId)
                };

                var fragmentos = await _context.Database
                    .SqlQueryRaw<StoreProcedureResult>("EXEC sp_ObtenerFormularioPorId @PlantillaId", parametros)
                    .ToListAsync();

                var json = string.Join("", fragmentos.Select(f => f.Mensaje));

                if (string.IsNullOrWhiteSpace(json) || json == "Formulario no encontrado")
                    return OperationResult<FormularioDetalleDTO?>.Fail("Formulario no encontrado");

                var data = JsonSerializer.Deserialize<FormularioDetalleDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return OperationResult<FormularioDetalleDTO?>.Ok(data, "OK");
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioDetalleDTO?>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<FormularioDetalleDTO?>> ObtenerFormularioPorPublicId(Guid publicId)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
            new SqlParameter("@PublicId", publicId)
                };

                var fragmentos = await _context.Database
                    .SqlQueryRaw<StoreProcedureResult>("EXEC sp_ObtenerFormularioPorPublicId @PublicId", parametros)
                    .ToListAsync();

                var json = string.Join("", fragmentos.Select(f => f.Mensaje));

                if (string.IsNullOrWhiteSpace(json) || json == "Formulario no encontrado o no publicado")
                    return OperationResult<FormularioDetalleDTO?>.Fail("Formulario no encontrado");

                var data = JsonSerializer.Deserialize<FormularioDetalleDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return OperationResult<FormularioDetalleDTO?>.Ok(data, "OK");
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioDetalleDTO?>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<FormularioRespuestasDTO?>> ObtenerRespuestasFormulario(int plantillaId)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
            new SqlParameter("@PlantillaId", plantillaId)
                };

                var fragmentos = await _context.Database
                    .SqlQueryRaw<StoreProcedureResult>("EXEC sp_ObtenerRespuestasFormulario @PlantillaId", parametros)
                    .ToListAsync();

                var json = string.Join("", fragmentos.Select(f => f.Mensaje));

                if (string.IsNullOrWhiteSpace(json) || json == "Sin respuestas")
                    return OperationResult<FormularioRespuestasDTO?>.Ok(new FormularioRespuestasDTO(), "Sin respuestas");

                var data = JsonSerializer.Deserialize<FormularioRespuestasDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return OperationResult<FormularioRespuestasDTO?>.Ok(data, "OK");
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioRespuestasDTO?>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<FormularioResultDTO>> PublicarFormulario(int plantillaId, bool publicar)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
                new SqlParameter("@PlantillaId", plantillaId),
                new SqlParameter("@Publicar",    publicar)
                };

                var resultado = await _context.Database
                    .SqlQueryRaw<FormularioResultDTO>(
                        "EXEC sp_PublicarFormulario @PlantillaId, @Publicar",
                        parametros)
                    .ToListAsync();

                var sp = resultado.FirstOrDefault();
                if (sp == null)
                    return OperationResult<FormularioResultDTO>.Fail("No se recibió respuesta del procedimiento.");

                return sp.ResultId > 0
                    ? OperationResult<FormularioResultDTO>.Ok(sp, sp.Mensaje)
                    : OperationResult<FormularioResultDTO>.Fail(sp.Mensaje);
            }
            catch (Exception ex)
            {
                return OperationResult<FormularioResultDTO>.Fail(ex.Message);
            }
        }

        private (DataTable preguntas, DataTable opciones) BuildTables(List<PreguntaFormularioDTO> preguntas)
        {
            var dtPreguntas = new DataTable();
            dtPreguntas.Columns.Add("TipoPregunta", typeof(string));
            dtPreguntas.Columns.Add("Etiqueta", typeof(string));
            dtPreguntas.Columns.Add("Obligatorio", typeof(bool));
            dtPreguntas.Columns.Add("Orden", typeof(int));

            var dtOpciones = new DataTable();
            dtOpciones.Columns.Add("PreguntaOrden", typeof(int));
            dtOpciones.Columns.Add("TextoOpcion", typeof(string));
            dtOpciones.Columns.Add("Orden", typeof(int));

            foreach (var p in preguntas)
            {
                dtPreguntas.Rows.Add(p.TipoPregunta, p.Etiqueta, p.Obligatorio, p.Orden);

                foreach (var o in p.Opciones)
                    dtOpciones.Rows.Add(p.Orden - 1, o.TextoOpcion, o.Orden);
            }

            return (dtPreguntas, dtOpciones);
        }
    }
}
