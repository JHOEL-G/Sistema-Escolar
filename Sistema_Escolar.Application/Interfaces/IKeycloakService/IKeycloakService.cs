using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IKeycloakService
{
    public interface IKeycloakService
    {
        Task<List<OperationResult>> SincronizarUsuariosDesdeKeycloak();
        Task<OperationResult> CrearUsuarioKeycloak(UsuarioDTO usuario, string password, Stream? imagenStream = null, string? nombreImagen = null);
        Task<OperationResult> CambiarPasswordKeycloak(Guid keycloakId, string nuevaPassword);
        Task<OperationResult> EliminarUsuarioKeycloak(Guid keycloakId);
    }
}
