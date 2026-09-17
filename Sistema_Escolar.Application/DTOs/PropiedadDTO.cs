using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PropiedadDTO
    {
        public int PropiedadId { get; set; }

        public string? NombrePropiedad { get; set; }

        public string? Etiqueta { get; set; }

        public int? TipoCampo { get; set; }

        public string? ValorDefecto { get; set; }

        public bool? EsRequerido { get; set; }

        public bool? UsarComoFiltro { get; set; }

        public bool? UsarEnReporte { get; set; }
    }
}
