using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoDetalleDTO
    {
        public int ModuloRecursoId { get; set; }
        public int ModuloId { get; set; }
        public int RecursoId { get; set; }
        public string NombreTipo { get; set; } = string.Empty;
        public string? Categoria { get; set; }
        public string Icono { get; set; } = string.Empty;
        public int OrdenRecurso { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? Ponderacion { get; set; }

        public string TituloRecurso { get; set; } = string.Empty;
        public string? DatosDetalle { get; set; } = string.Empty;
    }
}
