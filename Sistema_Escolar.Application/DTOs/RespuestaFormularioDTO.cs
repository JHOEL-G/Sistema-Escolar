using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RespuestaFormularioDTO
    {
        public int PreguntaId { get; set; }
        public string? TextoRespuesta { get; set; }
        public int? OpcionId { get; set; }
    }
}
