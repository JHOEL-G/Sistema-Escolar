using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class OuService : IOuService
    {
        private readonly IOuRespository _repo;

        public OuService(IOuRespository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearOu(OuDTO ouDTO)
        {
            if (string.IsNullOrWhiteSpace(ouDTO.Nombre)) return OperationResult.Fail("El nombre no puede estar vacio");

            var resultado = await _repo.InsertarOu(ouDTO);
            return resultado;
        }

        public async Task<OperationResult> EditarOu(OuDTO ouDTO)
        {
            if (ouDTO.OrganizacionalesId <= 0) return OperationResult.Fail("El id no es valido");

            return await _repo.UpdatearOu(ouDTO);
        }

        public async Task<OperationResult<OuDTO?>> ObtenerOuId(int id)
        {
            var ou = await _repo.GetOuById(id);

            if (ou == null) return OperationResult<OuDTO?>.Fail("OU no encontrado");

            return OperationResult<OuDTO?>.Ok(ou, "OU obtenido correctamente");
        }

        public async Task<OperationResult<IEnumerable<OuDTO>>> ObtenerOus()
        {
            var ous = await _repo.GetOu();

            return OperationResult<IEnumerable<OuDTO>>.Ok(ous, "OUs obtenidos correctamente");
        }
    }
}
