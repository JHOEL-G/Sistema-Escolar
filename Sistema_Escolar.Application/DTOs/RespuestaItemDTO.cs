using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RespuestaItemDTO
    {
        public int? PreguntaId { get; set; }        
        public int? OpcionId { get; set; }
        public string? TextoRespuesta { get; set; }
        public bool EsCorrecta { get; set; }
        public decimal PuntosObtenidos { get; set; }
        public string? TextoPregunta { get; set; }  
        public string? TextoOpcion { get; set; }    
        public int? TipoPreguntaId { get; set; }
    }
}
