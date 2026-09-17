using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IRolService
    {
        Task<OperationResult> CrearRol(CatRolDTO rolDTO);
        Task<OperationResult> EditarRol(CatRolDTO rolDTO);
        Task<OperationResult<IEnumerable<CatRolDTO>>> ObtenerRoles();
        Task<OperationResult<CatRolDTO?>> ObtenerRolId(int id);
    }
}
