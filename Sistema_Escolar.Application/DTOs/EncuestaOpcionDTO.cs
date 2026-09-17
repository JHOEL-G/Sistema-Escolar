using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class EncuestaOpcionDTO
    {
        public int? EncuestaOpcionId { get; set; }
        public string TituloOpcion { get; set; } = string.Empty;
        public string TextoOpcion { get; set; } = string.Empty;
        public int OrdenOpcion { get; set; }
    }
}
