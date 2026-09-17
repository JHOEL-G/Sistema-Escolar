using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuarios(bool? activo);
        Task<UsuarioDTO?> GetUsuarioById(int id);
        Task<OperationResult> InsertarUsuarioAsync(UsuarioDTO usuario, string contraseñaHash);
        Task<OperationResult> UpdateUsuario(UsuarioDTO usuario, string? contraseñaHash);
        Task<UsuarioDTO?> GetUsuarioByKeycloakId(Guid keycloakId);

        Task<IEnumerable<PuestoDTO>> GetPuesto();
        Task<IEnumerable<JefeDirectoDTO>> GetJefe();
        Task<OperationResult> DeleteUsuario(int id);
    }
}
