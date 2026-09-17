using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class ModulosRrecursosService : IModulosRecursosService
    {
        private readonly IModulosRecursosRepository _repo;

        public ModulosRrecursosService(IModulosRecursosRepository repo)
        {
            _repo = repo;
        }

        public async Task<object> AgregarModulosRecursos(AgregarModulosCursoDTO dto)
        {
            var resultado = await _repo.AgregarModulosRecursos(dto);

            if (resultado == null) return OperationResult.Fail("No se pudieron agregar los módulos y recursos al curso.");

            return OperationResult.Ok(resultado, "Módulos y recursos agregados exitosamente al curso.");
        }
    }
}
