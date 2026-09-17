using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CarpetaResponseDTO
    {
        public int ExploradorId { get; set; }
        public string NombreCarpeta { get; set; } = string.Empty;
        public int? PadreId { get; set; }
        public int Nivel { get; set; }
        public string Ruta { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
