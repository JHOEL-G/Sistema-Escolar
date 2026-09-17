using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ArchivoDTO
    {
        public string ArchivoPath { get; set; } = string.Empty;
        public string? NombreArchivo { get; set; }
        public string? TipoArchivo { get; set; }
        public int Orden { get; set; }
    }
}
