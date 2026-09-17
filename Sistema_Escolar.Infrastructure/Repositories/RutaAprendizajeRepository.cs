using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class RutaAprendizajeRepository : IRutaAprendizajeRepository
    {
        private readonly ConfiaContext _context;

        public RutaAprendizajeRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRutaAprendizaje(RutaAprendizajeDTO dto)
        {
            var participantesTable = new DataTable();
            participantesTable.Columns.Add("UsuarioId", typeof(int));
            foreach (var uid in dto.Participantes ?? new List<int>())
                participantesTable.Rows.Add(uid);

            var uosTable = new DataTable();
            uosTable.Columns.Add("EntidadId", typeof(int));
            uosTable.Columns.Add("TipoEntidad", typeof(string));
            foreach (var item in dto.SeleccionadosPrivacidad ?? new List<SeleccionadoDTO>())
                uosTable.Rows.Add(item.Id, item.Tipo);

            var inscripcionTable = new DataTable();
            inscripcionTable.Columns.Add("EntidadId", typeof(int));
            inscripcionTable.Columns.Add("TipoEntidad", typeof(string));
            foreach (var item in dto.InscripcionAutomatica ?? new List<SeleccionadoDTO>())
                inscripcionTable.Rows.Add(item.Id, item.Tipo);

            var seccionesTable = new DataTable();
            seccionesTable.Columns.Add("NombreSeccion", typeof(string));
            seccionesTable.Columns.Add("Orden", typeof(int));
            seccionesTable.Columns.Add("CursoId", typeof(int));
            seccionesTable.Columns.Add("OrdenCurso", typeof(int));
            foreach (var seccion in dto.Secciones ?? new List<SeccionDTO>())
                for (int i = 0; i < seccion.CursoIds.Count; i++)
                    seccionesTable.Rows.Add(seccion.NombreSeccion, seccion.Orden, seccion.CursoIds[i], i + 1);

            var certificadoresTable = new DataTable();
            certificadoresTable.Columns.Add("UsuarioId", typeof(int));
            foreach (var uid in dto.Certificadores ?? new List<int>())
                certificadoresTable.Rows.Add(uid);

            var parametros = new SqlParameter[]
            {
        new SqlParameter("@NombreRuta",             dto.NombreRuta          ?? (object)DBNull.Value),
        new SqlParameter("@Descripcion",            dto.Descripcion         ?? (object)DBNull.Value),
        new SqlParameter("@ImagenPortada",          dto.ImagenPortada       ?? (object)DBNull.Value),
        new SqlParameter("@NombrePrivacidad",       dto.NombrePrivacidad    ?? (object)DBNull.Value),
        new SqlParameter("@Externo",                dto.Externo),
        new SqlParameter("@UOsPrivacidad",          SqlDbType.Structured) { Value = uosTable,         TypeName = "dbo.TipoUOsPrivacidad"  },
        new SqlParameter("@AsignarFecha",           dto.AsignarFecha        ?? (object)DBNull.Value),
        new SqlParameter("@AsignarDia",             dto.AsignarDia          ?? (object)DBNull.Value),
        new SqlParameter("@FechaLimite",            dto.FechaLimite),
        new SqlParameter("@SeccionesCursos",        SqlDbType.Structured) { Value = seccionesTable,   TypeName = "dbo.TipoSeccionesCursos"},
        new SqlParameter("@InscripcionAutomatica",  SqlDbType.Structured) { Value = inscripcionTable, TypeName = "dbo.TipoUOsPrivacidad"  }, 
        new SqlParameter("@PropiedadesId",          dto.PropiedadesId       ?? (object)DBNull.Value),
        new SqlParameter("@PermiteDesinscripcion",  dto.PermiteDesinscripcion),
        new SqlParameter("@NombreCertificado",      dto.NombreCertificado   ?? (object)DBNull.Value),
        new SqlParameter("@CondicionAvanceCurso",   dto.CondicionAvanceCurso),
        new SqlParameter("@CondicionAvanceSeccion", dto.CondicionAvanceSeccion),
        new SqlParameter("@CriterioAprobacion",     dto.CriterioAprobacion  ?? (object)DBNull.Value),
        new SqlParameter("@Gamificacion",           dto.Gamificacion        ?? (object)DBNull.Value),
        new SqlParameter("@MensajeBienvenida",      dto.MensajeBienvenida   ?? (object)DBNull.Value),
        new SqlParameter("@Participantes",          SqlDbType.Structured) { Value = participantesTable, TypeName = "dbo.TipoParticipantes"},
                        new SqlParameter("@Certificadores",         SqlDbType.Structured) { Value = certificadoresTable, TypeName = "dbo.TipoCertificadores"   },

            };

            var resultado = await _context.Database
                .SqlQueryRaw<StoreProcedureResult>(
                    @"EXEC sp_Crear_Ruta_Aprendizaje 
                      @NombreRuta, @Descripcion, @ImagenPortada,
                      @NombrePrivacidad, @Externo, @UOsPrivacidad,
                      @AsignarFecha, @AsignarDia, @FechaLimite, @SeccionesCursos,
                      @InscripcionAutomatica, @PropiedadesId, @PermiteDesinscripcion,
                      @NombreCertificado, @CondicionAvanceCurso, @CondicionAvanceSeccion,
                      @CriterioAprobacion, @Gamificacion, @MensajeBienvenida,
                      @Participantes, @Certificadores",
                    parametros)
                .ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null)
                return OperationResult.Fail("No se recibió respuesta del procedimiento almacenado");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<IEnumerable<ParticipanteRutaDTO>> GetParticipantesRuta(int rutaId)
        {
            var resultado = await _context.Database
                .SqlQueryRaw<ParticipanteRutaDTO>("EXEC sp_ObtenerParticipantesRuta @RutaId",
                    new SqlParameter("@RutaId", rutaId))
                .ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<ListarRutaAprendizajeDTO>> GetRutaAprendizaje()
        {
            var resultado = await _context.Database
                .SqlQueryRaw<ListarRutaAprendizajeDTO>("EXEC sp_ObtenerTodoAprendizaje")
                .ToListAsync();
            return resultado;
        }

        public async Task<RutaAprendizajeDetalleDTO?> GetRutaById(int rutaId)
        {
            var resultado = await _context.Database
               .SqlQueryRaw<RutaAprendizajeDetalleDTO>("EXEC sp_ObtenerRutaAprendizajePorId @RutaId",
                   new SqlParameter("@RutaId", rutaId))
               .ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<(IEnumerable<RutaUsuarioDTO> Rutas, IEnumerable<CursoProgresoDTO> Cursos)> GetRutasPorUsuario(int usuarioId)
        {
            var rutas = new List<RutaUsuarioDTO>();
            var cursos = new List<CursoProgresoDTO>();

            var conn = _context.Database.GetDbConnection();
            var wasOpen = conn.State == System.Data.ConnectionState.Open;
            if (!wasOpen) await conn.OpenAsync();

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "EXEC sp_ObtenerRutasPorUsuario @UsuarioId";
                var param = cmd.CreateParameter();
                param.ParameterName = "@UsuarioId";
                param.Value = usuarioId;
                cmd.Parameters.Add(param);

                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    rutas.Add(new RutaUsuarioDTO
                    {
                        RutaId = reader.GetInt32(reader.GetOrdinal("RutaId")),
                        NombreRuta = reader["NombreRuta"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("NombreRuta")),
                        Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                        ImagenPortada = reader["ImagenPortada"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("ImagenPortada")),
                        FechaCreacion = reader["FechaCreacion"] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                        FechaInscripcion = reader["FechaInscripcion"] == DBNull.Value ? null : reader.GetDateTime(reader.GetOrdinal("FechaInscripcion")),
                        Progreso = reader.GetDecimal(reader.GetOrdinal("Progreso")),
                        Completado = reader.GetBoolean(reader.GetOrdinal("Completado")),
                        CalificacionFinal = reader["CalificacionFinal"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("CalificacionFinal")),
                        TotalSecciones = reader.GetInt32(reader.GetOrdinal("TotalSecciones")),
                        TotalCursos = reader.GetInt32(reader.GetOrdinal("TotalCursos")),
                        CursosCompletados = reader.GetInt32(reader.GetOrdinal("CursosCompletados")),
                        NombreCertificado = reader["NombreCertificado"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("NombreCertificado")),
                        CondicionAvanceCurso = reader["CondicionAvanceCurso"] == DBNull.Value ? null : reader.GetBoolean(reader.GetOrdinal("CondicionAvanceCurso")),
                        CondicionAvanceSeccion = reader["CondicionAvanceSeccion"] == DBNull.Value ? null : reader.GetBoolean(reader.GetOrdinal("CondicionAvanceSeccion")),
                        CriterioAprobacion = reader["CriterioAprobacion"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("CriterioAprobacion")),
                        MensajeBienvenida = reader["MensajeBienvenida"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("MensajeBienvenida")),
                    });
                }

                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                {
                    cursos.Add(new CursoProgresoDTO
                    {
                        RutaId = reader.GetInt32(reader.GetOrdinal("RutaId")),
                        SeccionId = reader.GetInt32(reader.GetOrdinal("SeccionId")),
                        CursoId = reader.GetInt32(reader.GetOrdinal("CursoId")),
                        Progreso = reader.GetDecimal(reader.GetOrdinal("Progreso")),
                        EsCompletado = reader.GetBoolean(reader.GetOrdinal("EsCompletado")),
                        CalificacionFinal = reader["CalificacionFinal"] == DBNull.Value ? null : reader.GetDecimal(reader.GetOrdinal("CalificacionFinal")),
                    });
                }
            }
            finally
            {
                if (!wasOpen) await conn.CloseAsync();
            }

            return (rutas, cursos);
        }

        public async Task<OperationResult> UpdateRutaAprendizaje(int rutaId, RutaAprendizajeDTO dto)
        {
            var participantesTable = new DataTable();
            participantesTable.Columns.Add("UsuarioId", typeof(int));
            foreach (var uid in dto.Participantes ?? new List<int>())
                participantesTable.Rows.Add(uid);

            var uosTable = new DataTable();
            uosTable.Columns.Add("EntidadId", typeof(int));
            uosTable.Columns.Add("TipoEntidad", typeof(string));
            foreach (var item in dto.SeleccionadosPrivacidad ?? new List<SeleccionadoDTO>())
                uosTable.Rows.Add(item.Id, item.Tipo);

            var inscripcionTable = new DataTable();
            inscripcionTable.Columns.Add("EntidadId", typeof(int));
            inscripcionTable.Columns.Add("TipoEntidad", typeof(string));
            foreach (var item in dto.InscripcionAutomatica ?? new List<SeleccionadoDTO>())
                inscripcionTable.Rows.Add(item.Id, item.Tipo);

            var seccionesTable = new DataTable();
            seccionesTable.Columns.Add("NombreSeccion", typeof(string));
            seccionesTable.Columns.Add("Orden", typeof(int));
            seccionesTable.Columns.Add("CursoId", typeof(int));
            seccionesTable.Columns.Add("OrdenCurso", typeof(int));
            foreach (var seccion in dto.Secciones ?? new List<SeccionDTO>())
                for (int i = 0; i < seccion.CursoIds.Count; i++)
                    seccionesTable.Rows.Add(seccion.NombreSeccion, seccion.Orden, seccion.CursoIds[i], i + 1);

            var certificadoresTable = new DataTable();
            certificadoresTable.Columns.Add("UsuarioId", typeof(int));
            foreach (var uid in dto.Certificadores ?? new List<int>())
                certificadoresTable.Rows.Add(uid);

            var parametros = new SqlParameter[]
            {
        new SqlParameter("@RutaId",                 rutaId),
        new SqlParameter("@NombreRuta",             dto.NombreRuta          ?? (object)DBNull.Value),
        new SqlParameter("@Descripcion",            dto.Descripcion         ?? (object)DBNull.Value),
        new SqlParameter("@ImagenPortada",          dto.ImagenPortada       ?? (object)DBNull.Value),
        new SqlParameter("@NombrePrivacidad",       dto.NombrePrivacidad    ?? (object)DBNull.Value),
        new SqlParameter("@Externo",                dto.Externo),
        new SqlParameter("@UOsPrivacidad",          SqlDbType.Structured) { Value = uosTable,          TypeName = "dbo.TipoUOsPrivacidad"  },
        new SqlParameter("@AsignarFecha",           dto.AsignarFecha        ?? (object)DBNull.Value),
        new SqlParameter("@AsignarDia",             dto.AsignarDia          ?? (object)DBNull.Value),
        new SqlParameter("@FechaLimite",            dto.FechaLimite),
        new SqlParameter("@SeccionesCursos",        SqlDbType.Structured) { Value = seccionesTable,    TypeName = "dbo.TipoSeccionesCursos"},
        new SqlParameter("@InscripcionAutomatica",  SqlDbType.Structured) { Value = inscripcionTable,  TypeName = "dbo.TipoUOsPrivacidad"  }, 
        new SqlParameter("@PropiedadesId",          dto.PropiedadesId       ?? (object)DBNull.Value),
        new SqlParameter("@PermiteDesinscripcion",  dto.PermiteDesinscripcion),
        new SqlParameter("@NombreCertificado",      dto.NombreCertificado   ?? (object)DBNull.Value),
        new SqlParameter("@CondicionAvanceCurso",   dto.CondicionAvanceCurso),
        new SqlParameter("@CondicionAvanceSeccion", dto.CondicionAvanceSeccion),
        new SqlParameter("@CriterioAprobacion",     dto.CriterioAprobacion  ?? (object)DBNull.Value),
        new SqlParameter("@Gamificacion",           dto.Gamificacion        ?? (object)DBNull.Value),
        new SqlParameter("@MensajeBienvenida",      dto.MensajeBienvenida   ?? (object)DBNull.Value),
        new SqlParameter("@Participantes",          SqlDbType.Structured) { Value = participantesTable, TypeName = "dbo.TipoParticipantes"},
                        new SqlParameter("@Certificadores",         SqlDbType.Structured) { Value = certificadoresTable, TypeName = "dbo.TipoCertificadores"   },
            };

            var resultado = await _context.Database
                .SqlQueryRaw<StoreProcedureResult>(
                    @"EXEC sp_Modificar_Ruta_Aprendizaje
                      @RutaId, @NombreRuta, @Descripcion, @ImagenPortada,
                      @NombrePrivacidad, @Externo, @UOsPrivacidad,
                      @AsignarFecha, @AsignarDia, @FechaLimite, @SeccionesCursos,
                      @InscripcionAutomatica, @PropiedadesId, @PermiteDesinscripcion,
                      @NombreCertificado, @CondicionAvanceCurso, @CondicionAvanceSeccion,
                      @CriterioAprobacion, @Gamificacion, @MensajeBienvenida,
                      @Participantes, @Certificadores",
                    parametros)
                .ToListAsync();

            var sp = resultado.FirstOrDefault();
            if (sp == null)
                return OperationResult.Fail("No se recibió respuesta del procedimiento almacenado");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
