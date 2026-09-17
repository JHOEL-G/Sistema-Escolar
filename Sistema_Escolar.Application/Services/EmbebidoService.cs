using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class EmbebidoService : IEmbebidoService
    {
        private readonly IRecursoEmbebidoRepository _repo;

        public EmbebidoService (IRecursoEmbebidoRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearEmbebido(RecursoEmbebidoDTO dTO)
        {
            var resultado = await _repo.CreateRecursoEmbebido(dTO);

            return resultado;
        }
    }
}
