using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PublicarForoDTO
    {
        public int ForoId { get; set; }
        public int UsuarioId { get; set; }
        public string? Titulo { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public int? PublicacionPadreId { get; set; }
        public DateTime? FechaPublicacion { get; set; }  
        public string? ArchivoPath { get; set; }
    }
}
