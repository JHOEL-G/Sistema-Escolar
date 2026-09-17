using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class DocumentoInsertarDTO
    {
        public string TituloDocumento { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? DocumentoPath { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaExpiracion { get; set; }
        public int ExploradorId { get; set; }
        public string Formatos { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
    }
}
