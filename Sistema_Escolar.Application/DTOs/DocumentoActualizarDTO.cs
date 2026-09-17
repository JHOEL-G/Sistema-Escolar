using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class DocumentoActualizarDTO
    {
        public int GestionId { get; set; }
        public string TituloDocumento { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? DocumentoPath { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaExpiracion { get; set; }
        public int ExploradorId { get; set; }
        public bool Activo { get; set; }
        public List<int> Formatos { get; set; } = new();
        public List<int> Roles { get; set; } = new();
    }
}
