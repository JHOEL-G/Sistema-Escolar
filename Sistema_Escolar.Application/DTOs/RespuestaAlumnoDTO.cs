using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RespuestaAlumnoDTO
    {
        public int RespuestaId { get; set; }
        public int? PreguntaId { get; set; }
        public int? EvaluacionPreguntaId { get; set; }
        public string? TextoPregunta { get; set; }
        public int TipoPreguntaId { get; set; }
        public decimal PuntosValor { get; set; }
        public int? OpcionId { get; set; }
        public string? TextoOpcionSeleccionada { get; set; }
        public bool? OpcionEsCorrecta { get; set; }
        public bool EsCorrecta { get; set; }
        public decimal PuntosObtenidos { get; set; }
        public int Intento { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public string? TextoRespuesta { get; set; }
    }
}
