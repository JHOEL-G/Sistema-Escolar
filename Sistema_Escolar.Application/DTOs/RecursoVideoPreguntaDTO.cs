using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoVideoPreguntaDTO
    {
        public string? VideoPath { get; set; }
        public string? VideoLink { get; set; }
        public string? TipoSubida { get; set; }
        public bool? HacerVisibleDashboard { get; set; }

        public List<PreguntaVideoDTO>? Preguntas { get; set; }
    }
}
