using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoEvaluacionDTO
    {
        public int? EvaluacionId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string? Instrucciones { get; set; }
        public string? Descripcion { get; set; }
        public bool? AgregarPonderacion { get; set; } 
        public bool? PreguntasAleatorias { get; set; }
        public int? Oportunidades { get; set; } 
        public string PermitirReinicio { get; set; } = "No reiniciar";
        public int? PreguntasCorrectasAprobar { get; set; }
        public int? TiempoHoras { get; set; }
        public int? TipoCalificacionId { get; set; } = 1;

        public int? TiempoMinutos { get; set; }
        public List<BancaPreguntaSeleccionadaDTO>? BancasIds { get; set; }
        public List<BancaPreguntaSeleccionadaDTO>? BancasPreguntas { get; set; }

        public List<PreguntaManualDTO> Preguntas { get; set; } = new();

    }
}
