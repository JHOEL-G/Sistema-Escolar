using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class OpcionDetalleDTO
    {
        public int OpcionId { get; set; }
        public string TextoOpcion { get; set; } = string.Empty;
        public int Orden { get; set; }
    }
}
