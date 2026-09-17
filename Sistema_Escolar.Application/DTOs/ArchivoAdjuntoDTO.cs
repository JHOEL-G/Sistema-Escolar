using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ArchivoAdjuntoDTO
    {
        public int? ArchivoAdjuntoId { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaArchivo { get; set; } = string.Empty;
        public string TipoArchivo { get; set; } = string.Empty;
        public decimal TamañoMB { get; set; }
    }
}
