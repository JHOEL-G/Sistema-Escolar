using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RutaUsuarioDTO
    {
        public int RutaId { get; set; }
        public string? NombreRuta { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenPortada { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaInscripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Progreso { get; set; } 
        public bool? Completado { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CalificacionFinal { get; set; }

        public int TotalSecciones { get; set; }
        public int TotalCursos { get; set; }
        public int CursosCompletados { get; set; }

        public string? NombreCertificado { get; set; }

        public bool? CondicionAvanceCurso { get; set; }
        public bool? CondicionAvanceSeccion { get; set; }
        public string? CriterioAprobacion { get; set; }
        public string? MensajeBienvenida { get; set; }
    }
}
