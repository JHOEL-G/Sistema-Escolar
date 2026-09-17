using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PreguntaDetalleDTO
    {
        public int PreguntaId { get; set; }
        public string TipoPregunta { get; set; } = string.Empty;
        public string Etiqueta { get; set; } = string.Empty;
        public bool Obligatorio { get; set; }
        public int Orden { get; set; }
        public List<OpcionDetalleDTO> Opciones { get; set; } = new();
    }
}
