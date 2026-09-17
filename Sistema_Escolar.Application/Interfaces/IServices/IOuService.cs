using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IOuService
    {
        Task<OperationResult> CrearOu(OuDTO ouDTO);
        Task<OperationResult> EditarOu(OuDTO ouDTO);
        Task<OperationResult<IEnumerable<OuDTO>>> ObtenerOus();
        Task<OperationResult<OuDTO?>> ObtenerOuId(int id);
    }
}
