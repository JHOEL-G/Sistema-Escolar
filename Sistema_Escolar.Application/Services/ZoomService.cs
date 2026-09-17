using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class ZoomService : IZoomService
    {
        private readonly IRecursoZoomRepository _repo;

        public ZoomService (IRecursoZoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> CrearZoom(RecursoZoomDTO dTO)
        {
            var resultado = await _repo.CreateRecursoZoom(dTO);

            return resultado;
        }
    }
}
