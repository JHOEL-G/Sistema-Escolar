using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.Data.StoreProcedure;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConfiaContext _context;
        private readonly IMemoryCache _cache;

        public UsuarioRepository(ConfiaContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<UsuarioDTO?> GetUsuarioById(int id)
        {
            var cacheKey = $"usuario_id_{id}";

            if (_cache.TryGetValue(cacheKey, out UsuarioDTO? cachedUser)) return cachedUser;

            var resultado = await _context.Database
                .SqlQueryRaw<UsuarioDTO>(  
                    "EXEC sp_ObtenerUsuario @UsuarioId",
                    new SqlParameter("@UsuarioId", id))
                .ToListAsync();

            var usuario = resultado.FirstOrDefault();

            if (usuario != null) _cache.Set(cacheKey, usuario, TimeSpan.FromMinutes(10));

            return usuario;
        }
        
        public async Task<UsuarioDTO?> GetUsuarioByKeycloakId(Guid keycloakId)
        {
            var cacheKey = $"usuario_keycloak_{keycloakId}";

            if (_cache.TryGetValue(cacheKey, out UsuarioDTO? cachedUser)) return cachedUser;

            var parameter = new SqlParameter("@KeycloakId", keycloakId);

            var resultado = await _context.Database
                .SqlQueryRaw<UsuarioDTO>("EXEC sp_GetUsuarioByKeycloakId @KeycloakId", parameter)
                .ToListAsync();
            
            var usuario = resultado.FirstOrDefault();

            if (usuario != null) _cache.Set(cacheKey, usuario, TimeSpan.FromMinutes(10));

            return usuario;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuarios(bool? activo)
        {
            var cacheKey = $"usuarios_activo_{activo?.ToString() ?? "null"}";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<UsuarioDTO>? cachedUsuarios)) return cachedUsuarios!;

            var pActivo = new SqlParameter("@Activo", (object?)activo ?? DBNull.Value);

            var usuarios = await _context.Database
                .SqlQueryRaw<UsuarioDTO>(
                    "EXEC sp_ObtenerTodosUsuarios @Activo",
                    pActivo)
                .ToListAsync();

            _cache.Set(cacheKey, usuarios, TimeSpan.FromMinutes(5));

            return usuarios;
        }

        public async Task<OperationResult> InsertarUsuarioAsync(UsuarioDTO usuario, string contraseñaHash)
        {
            if (!usuario.KeycloakId.HasValue)
                return OperationResult.Fail("El ID de Keycloak es requerido");

            var parametros = new[]
            {
                new SqlParameter("@KeycloakId", usuario.KeycloakId),
                new SqlParameter("@Nombre", (object?)usuario.Nombre ?? DBNull.Value),
                new SqlParameter("@Apellido", (object?)usuario.ApeLLido ?? DBNull.Value), 
                new SqlParameter("@Correo", usuario.Correo),
                new SqlParameter("@ContraseñaHash", (object?)contraseñaHash ?? DBNull.Value),
                new SqlParameter("@PuestoId", (object?)usuario.PuestoId ?? DBNull.Value),
                new SqlParameter("@OrganizacionalesId", (object?)usuario.OrganizacionalesId ?? DBNull.Value),
                new SqlParameter("@JefeId", (object?)usuario.JefeId ?? DBNull.Value),
                new SqlParameter("@CorreoAlternativo", (object?)usuario.CorreoAlternativo ?? DBNull.Value),
                new SqlParameter("@Curp", (object?)usuario.Curp ?? DBNull.Value),
                DateParam("@FechaActivacion",    usuario.FechaActivacion),
                DateParam("@FechaDesactivacion", usuario.FechaDesactivacion),
                DateParam("@FechaNacimiento",    usuario.FechaNacimiento),
                new SqlParameter("@IdEmpleado", (object?)usuario.IdEmpleado ?? DBNull.Value),
                new SqlParameter("@RazonSocial", (object?)usuario.RazonSocial ?? DBNull.Value), 
                new SqlParameter("@RolId", (object?)usuario.RolId ?? DBNull.Value),
                new SqlParameter("@NivelPermisoId", (object?)usuario.NivelPermisoId ?? DBNull.Value),
                new SqlParameter("@ImagenPortada", (object?)usuario.ImagenPortada ?? DBNull.Value)
            };
            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                @"EXEC sp_InsertarUsuario 
                @KeycloakId, @Nombre, @Apellido, @Correo, @ContraseñaHash,
                @PuestoId, @OrganizacionalesId, @JefeId, @CorreoAlternativo,
                @Curp, @FechaActivacion, @FechaDesactivacion, @FechaNacimiento,
                @IdEmpleado, @RazonSocial, @RolId, @NivelPermisoId, @ImagenPortada",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null)
                return OperationResult.Fail("Error en la base de datos");

            if (sp.ResultId > 0) InvalidarCachesUsuarios();

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<OperationResult> UpdateUsuario(UsuarioDTO usuario, string? contraseñaHash)
        {
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", usuario.UsuarioId),
                new SqlParameter("@Nombre", (object?)usuario.Nombre ?? DBNull.Value),
                new SqlParameter("@Apellido", (object?)usuario.ApeLLido ?? DBNull.Value),
                new SqlParameter("@Correo", usuario.Correo),
                new SqlParameter("@ContraseñaHash", (object?)contraseñaHash ?? DBNull.Value),
                new SqlParameter("@PuestoId", (object?)usuario.PuestoId ?? DBNull.Value),
                new SqlParameter("@OrganizacionalesId", (object?)usuario.OrganizacionalesId ?? DBNull.Value),
                new SqlParameter("@JefeId", (object?)usuario.JefeId ?? DBNull.Value),
                new SqlParameter("@RolId", (object?)usuario.RolId ?? DBNull.Value),
                new SqlParameter("@CorreoAlternativo", (object?)usuario.CorreoAlternativo ?? DBNull.Value),
                new SqlParameter("@Curp", (object?)usuario.Curp ?? DBNull.Value),
                DateParam("@FechaActivacion",    usuario.FechaActivacion),
                DateParam("@FechaDesactivacion", usuario.FechaDesactivacion),
                DateParam("@FechaNacimiento",    usuario.FechaNacimiento),
                new SqlParameter("@IdEmpleado", (object?)usuario.IdEmpleado ?? DBNull.Value),
                new SqlParameter("@RazonSocial", DBNull.Value),
                new SqlParameter("@NivelPermisoId", (object?)usuario.NivelPermisoId ?? DBNull.Value),
                new SqlParameter("@Activo", (object?)usuario.Activo ?? true),
                new SqlParameter("@ImagenPortada", (object?)usuario.ImagenPortada ?? DBNull.Value)
            };
            var resultado = await _context.Database.SqlQueryRaw<ModificarUsuarioSpResult>(
                @"EXEC sp_ModificarUsuario 
                @UsuarioId, @Nombre, @Apellido, @Correo, @ContraseñaHash,
                @PuestoId, @OrganizacionalesId, @JefeId, @RolId,
                @CorreoAlternativo, @Curp, @FechaActivacion, @FechaDesactivacion,
                @FechaNacimiento, @IdEmpleado, @RazonSocial, @NivelPermisoId, @Activo, @ImagenPortada",
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null)
                return OperationResult.Fail("Error en la base de datos");

            if (sp.Resultado == 1)
            {
                InvalidarCachesUsuarios();

                _cache.Remove($"usuario_id_{usuario.UsuarioId}");
                if (usuario.KeycloakId.HasValue) _cache.Remove($"usuario_keycloak_{usuario.KeycloakId.Value}");
            }

            return sp.Resultado == 1
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        private void InvalidarCachesUsuarios()
        {
            _cache.Remove("usuarios_activo_True");
            _cache.Remove("usuarios_activo_False");
            _cache.Remove("usuarios_activo_null");
        }

        private static SqlParameter DateParam(string name, DateTime? value)
        {
            var param = new SqlParameter(name, SqlDbType.Date);
            param.Value = value.HasValue ? (object)value.Value.Date : DBNull.Value;
            return param;
        }

        public async Task<IEnumerable<PuestoDTO>> GetPuesto()
        {
            var resultado = await _context.Database.SqlQueryRaw<PuestoDTO>(
                "EXEC sp_ObtenerTodosPuesto").ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<JefeDirectoDTO>> GetJefe()
        {
            var resultado = await _context.Database.SqlQueryRaw<JefeDirectoDTO>(
                "EXEC sp_ObtenerTodosJefe").ToListAsync();

            return resultado;
        }

        public async Task<OperationResult> DeleteUsuario(int id)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.UsuarioId == id);
            if (!existe)
                return OperationResult.Fail("El usuario no existe.");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EliminarUsuario @UsuarioId",
                new SqlParameter("@UsuarioId", id));

            InvalidarCachesUsuarios();
            _cache.Remove($"usuario_id_{id}");

            return OperationResult.Ok("Usuario eliminado correctamente.");
        }
    }
}
