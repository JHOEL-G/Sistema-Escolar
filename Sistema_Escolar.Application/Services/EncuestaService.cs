using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class EncuestaService : IEncuestaService
    {
        private readonly IRecursoEncuestaRepository _repo;

        public EncuestaService (IRecursoEncuestaRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearEncuesta(RecursoEncuestaDTO dTO)
        {
            var reultado = await _repo.CreateRecursoEncuesta(dTO);

            return reultado;
        }
    }
}
