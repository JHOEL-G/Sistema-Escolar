using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class GuardarRespuestasDTO
    {
        public int EvaluacionId { get; set; }
        public int UsuarioId { get; set; }
        public List<RespuestaItemDTO>? Respuestas { get; set; }
    }
}
