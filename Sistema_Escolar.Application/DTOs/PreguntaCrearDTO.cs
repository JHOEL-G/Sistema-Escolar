using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PreguntaCrearDTO
    {
        public int TipoPreguntaId { get; set; } 
        public string TextoPregunta { get; set; } = string.Empty;
        public string Explicacion { get; set; } = string.Empty;
        public decimal PuntosValor { get; set; }
        public int Orden { get; set; }
        public List<OpcionCrearDTO> Opciones { get; set; } = new();
    }
}
