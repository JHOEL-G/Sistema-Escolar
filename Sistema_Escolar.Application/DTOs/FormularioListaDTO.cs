using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class FormularioListaDTO
    {
        public int PlantillaId { get; set; }
        public Guid PublicId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Publicado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public int TotalPreguntas { get; set; }
    }
}
