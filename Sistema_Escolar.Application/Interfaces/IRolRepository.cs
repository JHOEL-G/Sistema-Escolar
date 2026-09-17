using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<CatRolDTO>> GetRoles();
        Task<CatRolDTO?> GetRolById(int id);
        Task<OperationResult> InsertarRol(CatRolDTO catRol);
        Task<OperationResult> UpdatearRol(CatRolDTO catRol);
    }
}
