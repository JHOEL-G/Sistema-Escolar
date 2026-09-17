using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class UsuarioCursoDTO
    {
        public int InscripcionId { get; set; }

        public int UsuarioId { get; set; }

        public int CursoId { get; set; }

        public DateTime? FechaInscripcion { get; set; }

        [Column(TypeName = "decimal(5,2)")]  
        public decimal? Progreso { get; set; }

        [Column(TypeName = "decimal(5,2)")]  
        public decimal? CalificacionFinal { get; set; }

        public bool? EsCompletado { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        public string? NombreCurso { get; set; }
        public string? DescripcionCurso { get; set; }
        public string? ImagenPortadaPath { get; set; }
    }
}
