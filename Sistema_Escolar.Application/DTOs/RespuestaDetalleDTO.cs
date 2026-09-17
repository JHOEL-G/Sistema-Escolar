using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RespuestaDetalleDTO
    {
        public int RespuestaId { get; set; }
        public int PreguntaId { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public string TipoPregunta { get; set; } = string.Empty;
        public string? TextoRespuesta { get; set; }
        public string? OpcionSeleccionada { get; set; }
    }
}
