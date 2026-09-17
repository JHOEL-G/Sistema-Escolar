using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IPropiedadService
    {
        Task<OperationResult> CrearPropiedad(PropiedadDTO propiedadDTO);
        Task<OperationResult> EditarPropiedad(PropiedadDTO propiedadDTO);
        Task<OperationResult<IEnumerable<PropiedadDTO>>> ObtenerPropiedades();
        Task<OperationResult<PropiedadDTO?>> ObtenerPropiedadId(int id);
    }
}
