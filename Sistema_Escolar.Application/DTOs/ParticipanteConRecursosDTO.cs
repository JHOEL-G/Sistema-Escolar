using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ParticipanteConRecursosDTO
    {
        public int InscripcionId { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApeLLido { get; set; } = string.Empty;
        public string? Correo { get; set; }
        public string? ImagenPortada { get; set; }
        public decimal Progreso { get; set; }
        public decimal CalificacionFinal { get; set; }
        public bool EsCompletado { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }

        public List<RecursoCalificacionDTO> Recursos { get; set; } = new();
    }
}
