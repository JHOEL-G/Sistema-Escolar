using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CarpetaInsertarDTO
    {
        public string NombreCarpeta { get; set; } = string.Empty;
        public int? PadreId { get; set; }
    }
}
