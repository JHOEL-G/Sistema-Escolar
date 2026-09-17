using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class OpcionManualDTO
    {
        public int? EvaluacionOpcionId { get; set; }
        public string TextoOpcion { get; set; } = "";
        public bool EsCorrecta { get; set; }
        public string ExplicacionORelacion { get; set; } = "";
        public string? ImagenOpcion { get; set; }
    }
}
