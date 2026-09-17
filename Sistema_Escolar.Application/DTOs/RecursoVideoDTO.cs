using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoVideoDTO
    {
        public int? VideoId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string? VideoPath { get; set; }
        public string? VideoLink { get; set; }
        public string TipoSubida { get; set; } = "Archivo";
        public bool? HacerVisibleDashboard { get; set; }
        public string? Descripcion { get; set; }
    }
}
