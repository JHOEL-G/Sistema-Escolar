using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PreguntaManualDTO
    {
        public int? EvaluacionPreguntaId { get; set; } 
        public string TextoPregunta { get; set; } = "";
        public int TipoPreguntaId { get; set; } = 1;
        public decimal PuntosValor { get; set; } = 1;
        public bool Activo { get; set; } = true;
        public string? ImagenPregunta { get; set; }
        public List<OpcionManualDTO> Opciones { get; set; } = new();
    }
}
