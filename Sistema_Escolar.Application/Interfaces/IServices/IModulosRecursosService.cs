using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IModulosRecursosService
    {
        Task<object> AgregarModulosRecursos(AgregarModulosCursoDTO dto);
    }
}
