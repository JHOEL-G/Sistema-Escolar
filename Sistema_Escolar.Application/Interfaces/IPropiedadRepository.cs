using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IPropiedadRepository
    {
        Task<IEnumerable<PropiedadDTO>> GetPropiedades();
        Task<PropiedadDTO?> GetPropiedadById(int id);
        Task<OperationResult> InsertarPropiedad(PropiedadDTO propiedad);
        Task<OperationResult> UpdatePropiedad(PropiedadDTO propiedad);
    }
}
