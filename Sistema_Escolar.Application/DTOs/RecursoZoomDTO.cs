using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoZoomDTO
    {
        public int? ZoomId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string EnlaceZoom { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
