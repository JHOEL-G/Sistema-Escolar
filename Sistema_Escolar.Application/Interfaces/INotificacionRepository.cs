using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface INotificacionRepository
    {
        Task<OperationResult> Crear(CrearNotificacionDTO dto);
        Task<OperationResult> EliminarTodas(int usuarioId);
        Task<OperationResult<IEnumerable<NotificacionDTO>>> GetPorUsuario(int usuarioId);
        Task<OperationResult> MarcarLeida(int notificacionId);
        Task<int?> ObtenerUsuarioIdPorKeycloakId(Guid keycloakId);
        Task<IEnumerable<UsuarioNotificacionDTO>> ObtenerUsuariosParaNotificar(int gestionCursoId);
    }
}
