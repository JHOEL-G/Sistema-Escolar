using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PreguntaVideoDTO
    {
        public int PreguntaVideoId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string VideoPath { get; set; } = string.Empty;
        public string TextoPregunta { get; set; } = string.Empty;
        public int SegundoMarca { get; set; }
        public int TipoPreguntaId { get; set; }
        public decimal? PuntosValor { get; set; }
        public bool Activo { get; set; }

        public List<OpcionesPreguntaDTO> Opciones { get; set; } = new List<OpcionesPreguntaDTO>();
    }
}
