using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoEncuestaDTO
    {
        public int? EncuestaId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string? Instrucciones { get; set; }
        public string? Descripcion { get; set; }                        
        public List<BancaPreguntaSeleccionadaDTO>? BancasIds { get; set; }
        public List<EncuestaPreguntaDTO>? Preguntas { get; set; }
    }
}
