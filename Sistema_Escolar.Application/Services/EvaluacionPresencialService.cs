using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class EvaluacionPresencialService : IEvaluacionPresencialService
    {
        private readonly IRecursoEvaluacionPresencialRepository _repo;

        public EvaluacionPresencialService (IRecursoEvaluacionPresencialRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearEvaluacionPresencial(RecursoEvaluacionPresencialDTO dTO)
        {
            var resultado = await _repo.CreateRecursoEvaluacionPresencial(dTO);

            return resultado;
        }
    }
}
