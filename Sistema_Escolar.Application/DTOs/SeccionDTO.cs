using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class SeccionDTO
    {
        public string NombreSeccion { get; set; } = "Sección 1";
        public int Orden { get; set; }
        public List<int> CursoIds { get; set; } = new();
    }
}
