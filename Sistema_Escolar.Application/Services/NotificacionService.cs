using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _repo;
        private readonly IMemoryCache _cache;

        public NotificacionService(INotificacionRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<OperationResult> CrearNotificacion(CrearNotificacionDTO dto)
        {
            var resultado = await _repo.Crear(dto);
            if (resultado == null) return OperationResult.Fail("No se pudo crear la notificación");
            return resultado;
        }

        public async Task<OperationResult<IEnumerable<NotificacionDTO>>> ListarPorUsuario(int usuarioId)
        {
            var resultado = await _repo.GetPorUsuario(usuarioId);
            return resultado;
        }

        public async Task<OperationResult> MarcarLeida(int notificacionId)
        {
            var resultado = await _repo.MarcarLeida(notificacionId);
            return resultado;
        }

        public async Task<int?> ObtenerUsuarioIdPorKeycloakId(Guid keycloakId)
        {
            var cacheKey = $"notif_usuario_{keycloakId}";

            if (_cache.TryGetValue(cacheKey, out int cachedId))
                return cachedId;

            var resultado = await _repo.ObtenerUsuarioIdPorKeycloakId(keycloakId);

            if (resultado.HasValue)
                _cache.Set(cacheKey, resultado.Value, TimeSpan.FromMinutes(30));

            return resultado;
        }

        public async Task<OperationResult> EliminarTodas(int usuarioId)
            => await _repo.EliminarTodas(usuarioId);

        public async Task<IEnumerable<UsuarioNotificacionDTO>> ObtenerUsuariosParaNotificar(int gestionCursoId)
            => await _repo.ObtenerUsuariosParaNotificar(gestionCursoId);
    }
}
