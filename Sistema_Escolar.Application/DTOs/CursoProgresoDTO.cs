using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CursoProgresoDTO
    {
        public int RutaId { get; set; }
        public int SeccionId { get; set; }
        public int CursoId { get; set; }
        public decimal Progreso { get; set; }
        public bool EsCompletado { get; set; }
        public decimal? CalificacionFinal { get; set; }
    }
}
