using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;

        public RolService(IRolRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearRol(CatRolDTO rolDTO)
        {
            if (string.IsNullOrWhiteSpace(rolDTO.NombreRol)) return OperationResult.Fail("El nombre del rol no puede estar vacío.");

            var resultado = await _repo.InsertarRol(rolDTO);
            return resultado;
        }

        public async Task<OperationResult> EditarRol(CatRolDTO rolDTO)
        {
            if (rolDTO.RolId <= 0) return OperationResult.Fail("El ID del rol no es válido.");

            return await _repo.UpdatearRol(rolDTO);
        }

        public async Task<OperationResult<IEnumerable<CatRolDTO>>> ObtenerRoles()
        {
            var roles = await _repo.GetRoles();

            return OperationResult<IEnumerable<CatRolDTO>>.Ok(roles, "Rol obtenido correctamente");
        }

        public async Task<OperationResult<CatRolDTO?>> ObtenerRolId(int id)
        { 
            var rol = await _repo.GetRolById(id);

            if (rol == null) return OperationResult<CatRolDTO?>.Fail("Rol no encontrado");

            return OperationResult<CatRolDTO?>.Ok(rol, "Rol obtenido correctamente");
        }
    }
}
