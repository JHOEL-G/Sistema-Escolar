using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class AgregarModulosCursoDTO
    {
        public int CursoId { get; set; }
        public List<ModuloDTO> Modulos { get; set; } = new();
        public List<RecursoDTO> Recursos { get; set; } = new();
    }
}
