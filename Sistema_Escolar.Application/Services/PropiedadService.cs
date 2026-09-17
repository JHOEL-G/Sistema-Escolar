using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class PropiedadService : IPropiedadService
    {
        private readonly IPropiedadRepository _repo;

        public PropiedadService(IPropiedadRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearPropiedad(PropiedadDTO propiedadDTO)
        {
            if (string.IsNullOrWhiteSpace(propiedadDTO.NombrePropiedad)) return OperationResult.Fail("El nombre de la propiedad no puede estar vacío.");

            propiedadDTO.NombrePropiedad = propiedadDTO.NombrePropiedad.Replace(" ", "");

            var resultado = await _repo.InsertarPropiedad(propiedadDTO);
            return resultado;
        }

        public async Task<OperationResult> EditarPropiedad(PropiedadDTO propiedadDTO)
        {
            if (propiedadDTO.PropiedadId <= 0) return OperationResult.Fail("El id no es valido");

            return await _repo.UpdatePropiedad(propiedadDTO);
        }

        public async Task<OperationResult<IEnumerable<PropiedadDTO>>> ObtenerPropiedades()
        {
            var propiedades = await _repo.GetPropiedades();

            return OperationResult<IEnumerable<PropiedadDTO>>.Ok(propiedades, "Propiedades obtenido correctamente");
        }

        public async Task<OperationResult<PropiedadDTO?>> ObtenerPropiedadId(int id)
        {
            var propiedad = await _repo.GetPropiedadById(id);

            if (propiedad == null) return OperationResult<PropiedadDTO?>.Fail("Propiedad no encontrado");

            return OperationResult<PropiedadDTO?>.Ok(propiedad, "Propiedad obtenido correctamente");
        }
    }
}
