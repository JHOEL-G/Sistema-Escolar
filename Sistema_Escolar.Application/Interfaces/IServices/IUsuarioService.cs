using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;



namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IUsuarioService
    {
        Task<OperationResult> CrearUsuario(UsuarioDTO usuarioDto);
        Task<OperationResult> EditarUsuario(UsuarioDTO usuarioDto, Stream? imagenStream = null, string? nombreImagen = null);
        Task<OperationResult<IEnumerable<UsuarioDTO>>> ObtenerUsuario(bool? activo);
        Task<UsuarioDTO?> ObtenerUsuarioId(int id);
        Task<UsuarioDTO?> ObtenerPerfilLogin(Guid keycloakId);

        Task<OperationResult<IEnumerable<PuestoDTO>>> ObtenerPuesto();
        Task<OperationResult<IEnumerable<JefeDirectoDTO>>> ObtenerJefe();
        Task<OperationResult> EliminarUsuario(int id);
    }
}