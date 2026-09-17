using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class OpcionesPreguntaDTO
    {
        public int OpcionVideoId { get; set; }
        public string TextoOpcion { get; set; } = string.Empty;
        public bool EsCorrecta { get; set; }
    }
}
