using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
using Sistema_Escolar.Application.APIs;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IFileStorageService _service;


        public UsuarioService (IUsuarioRepository repo, IFileStorageService service)
        {
            _service = service;
            _repo = repo;
        }

        public async Task<OperationResult> CrearUsuario(UsuarioDTO usuarioDto)
        {
            if (string.IsNullOrEmpty(usuarioDto.Contraseña))
            {
                return OperationResult.Fail("La contraseña es requerida");
            }

            if (string.IsNullOrEmpty(usuarioDto.Correo))
            {
                return OperationResult.Fail("El correo es requerido");
            }

            string hash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contraseña);

            var resultado = await _repo.InsertarUsuarioAsync(usuarioDto, hash);

            return resultado;
        }

        public async Task<OperationResult> EditarUsuario(UsuarioDTO usuarioDto, Stream? imagenStream = null, string? nombreImagen = null)
        {
            if (usuarioDto.UsuarioId <= 0)
            {
                return OperationResult.Fail("ID de usuario inválido");
            }

            if (string.IsNullOrEmpty(usuarioDto.Correo))
            {
                return OperationResult.Fail("El correo es requerido");
            }

            string? contraseñaHash = null;
            if (!string.IsNullOrEmpty(usuarioDto.Contraseña))
            {
                contraseñaHash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contraseña);
            }

            if (imagenStream != null && !string.IsNullOrEmpty(nombreImagen))
            {
                var nuevaUrl = await _service.UploadFile(imagenStream, nombreImagen, "usuarios-avatar");
                usuarioDto.ImagenPortada = nuevaUrl;
            }
            else if (!string.IsNullOrEmpty(usuarioDto.ImagenPortada) && EsUrlCompleta(usuarioDto.ImagenPortada))
            {
                usuarioDto.ImagenPortada = null; 
            }

            return await _repo.UpdateUsuario(usuarioDto, contraseñaHash);
        }

        public async Task<OperationResult<IEnumerable<JefeDirectoDTO>>> ObtenerJefe()
        {
            var resultado = await _repo.GetJefe();

            return OperationResult<IEnumerable<JefeDirectoDTO>>.Ok(resultado, "Jefes obtenidos correctamente");
        }

        public async Task<UsuarioDTO?> ObtenerPerfilLogin(Guid keycloakId)
        {
            var usuario = await _repo.GetUsuarioByKeycloakId(keycloakId);
            if (usuario != null)
            {
                if (!string.IsNullOrEmpty(usuario.ImagenPortada) && !EsUrlCompleta(usuario.ImagenPortada))
                {
                    usuario.ImagenPortada = _service.GetPresignedUrl(usuario.ImagenPortada, 60);
                }
            }
            return usuario;
        }

        public async Task<OperationResult<IEnumerable<PuestoDTO>>> ObtenerPuesto()
        {
            var resultado = await _repo.GetPuesto();

            return OperationResult<IEnumerable<PuestoDTO>>.Ok(resultado, "Puestos obtenidos correctamente");
        }

        public async Task<OperationResult<IEnumerable<UsuarioDTO>>> ObtenerUsuario(bool? activo)
        {
            var usuarios = await _repo.GetUsuarios(activo);
            usuarios ??= new List<UsuarioDTO>();

            foreach (var u in usuarios)
            {
                if (!string.IsNullOrEmpty(u.ImagenPortada) && !EsUrlCompleta(u.ImagenPortada))
                    u.ImagenPortada = _service.GetPresignedUrl(u.ImagenPortada, 60);
            }

            if (!usuarios.Any())
                return OperationResult<IEnumerable<UsuarioDTO>>.Fail("No hay usuarios disponibles");

            return OperationResult<IEnumerable<UsuarioDTO>>.Ok(usuarios, "Usuarios obtenidos correctamente");
        }

        public async Task<UsuarioDTO?> ObtenerUsuarioId(int id)
        {
            var resultado = await _repo.GetUsuarioById(id);

            if (resultado != null)
            {
                if (!string.IsNullOrEmpty(resultado.ImagenPortada) && !EsUrlCompleta(resultado.ImagenPortada))
                {
                    resultado.ImagenPortada = _service.GetPresignedUrl(resultado.ImagenPortada, 60);
                }
            }

            return resultado;
        }

        private static bool EsUrlCompleta(string valor) =>
    valor.StartsWith("http://") || valor.StartsWith("https://");

        public async Task<OperationResult> EliminarUsuario(int id)
        {
            if (id <= 0) return OperationResult.Fail("ID de usuario inválido");

            return await _repo.DeleteUsuario(id);
        }
    }
}
