using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class OpcionCrearDTO
    {
        public string PreguntaTextoReferencia { get; set; } = string.Empty;
        public string TextoOpcion { get; set; } = string.Empty;
        public string ExplicacionORelacion { get; set; } = string.Empty;
        public bool EsCorrecta { get; set; }
        public int Orden { get; set; }
    }
}
