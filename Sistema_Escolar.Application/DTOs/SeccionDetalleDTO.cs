using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class SeccionDetalleDTO
    {
        public int SeccionId { get; set; }
        public string? NombreSeccion { get; set; }
        public int Orden { get; set; }
        public List<CursoDetalleDTO>? Cursos { get; set; }
    }
}
