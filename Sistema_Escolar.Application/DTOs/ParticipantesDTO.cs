using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ParticipantesDTO
    {
        public int? InscripcionId { get; set; }
        public int? UsuarioId { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? ApeLLido { get; set; } = string.Empty;
        public string? Correo { get; set; } = string.Empty;
        public DateTime? FechaInscripcion { get; set; }
        [Column(TypeName = "decimal(5,2)")] 
        public decimal? Progreso { get; set; }

        [Column(TypeName = "decimal(5,2)")]  
        public decimal? CalificacionFinal { get; set; }
        public bool? EsCompletado { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string? NombreRol { get; set; } = string.Empty;
        public string? NombreOU { get; set; } = string.Empty;
    }
}
