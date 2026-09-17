using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IModulosRecursosRepository
    {
        Task<object> AgregarModulosRecursos(AgregarModulosCursoDTO dto);
    }
}
