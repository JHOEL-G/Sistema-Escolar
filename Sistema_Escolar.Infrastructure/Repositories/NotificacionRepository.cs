using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly ConfiaContext _context;

        public NotificacionRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<IEnumerable<NotificacionDTO>>> GetPorUsuario(int usuarioId)
        {
            var resultado = await _context.Database
                .SqlQueryRaw<NotificacionDTO>(
                    "EXEC sp_ObtenerNotificacionesPorUsuario @UsuarioId",
                    new SqlParameter("@UsuarioId", usuarioId)
                )
                .ToListAsync();

            return OperationResult<IEnumerable<NotificacionDTO>>.Ok(resultado);
        }

        public async Task<OperationResult> MarcarLeida(int notificacionId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_MarcarNotificacionLeida @NotificacionId",
                new SqlParameter("@NotificacionId", notificacionId)
            );

            return OperationResult.Ok("Notificación marcada como leída.");
        }

        public async Task<OperationResult> Crear(CrearNotificacionDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@UsuarioId", dto.UsuarioId),
                new SqlParameter("@Tipo", dto.Tipo),
                new SqlParameter("@Mensaje", dto.Mensaje),
                new SqlParameter("@ReferenciaId", (object?)dto.ReferenciaId ?? DBNull.Value),
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CrearNotificacion @UsuarioId, @Tipo, @Mensaje, @ReferenciaId",
                parametros
            );

            return OperationResult.Ok("Notificación creada correctamente.");
        }

        public async Task<int?> ObtenerUsuarioIdPorKeycloakId(Guid keycloakId)
        {
            var resultado = await _context.Database
                .SqlQueryRaw<UsuarioDTO>(
                    "EXEC sp_GetUsuarioByKeycloakId @KeycloakId",
                    new SqlParameter("@KeycloakId", keycloakId)
                )
                .ToListAsync();

            return resultado.FirstOrDefault()?.UsuarioId;
        }

        public async Task<OperationResult> EliminarTodas(int usuarioId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EliminarNotificacionesPorUsuario @UsuarioId",
                new SqlParameter("@UsuarioId", usuarioId)
            );
            return OperationResult.Ok("Notificaciones eliminadas.");
        }

        public async Task<IEnumerable<UsuarioNotificacionDTO>> ObtenerUsuariosParaNotificar(int gestionCursoId)
        {
            return await _context.Database
                .SqlQueryRaw<UsuarioNotificacionDTO>(
                    "EXEC sp_ObtenerUsuariosParaNotificar @GestionCursoId",
                    new SqlParameter("@GestionCursoId", gestionCursoId)
                )
                .ToListAsync();
        }
    }
}
