using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class EncuestaPreguntaDTO
    {
        public int? EncuestaPreguntaId { get; set; }
        public string TextoPregunta { get; set; } = string.Empty;
        public string TipoPregunta { get; set; } = "OpcionMultiple";
        public int OrdenPregunta { get; set; }
        public int LimiteRespuestas { get; set; }
        public List<EncuestaOpcionDTO>? Opciones { get; set; }
    }
}
