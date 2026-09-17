using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class SesionPresencialService : ISesionPresencialService
    {
        private readonly IRecursoSesionPresencialRepository _repo;

        public SesionPresencialService ( IRecursoSesionPresencialRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearSesionPresencial(RecursoSesionPresencialDTO dTO)
        {
            var resultado = await _repo.CreateRecursoSesionPresencial(dTO);

            return resultado;
        }
    }
}
