using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IOuRespository
    {
        Task<IEnumerable<OuDTO>> GetOu();
        Task<OuDTO?> GetOuById(int id);
        Task<OperationResult> InsertarOu(OuDTO ou);
        Task<OperationResult> UpdatearOu(OuDTO ou);
    }
}
