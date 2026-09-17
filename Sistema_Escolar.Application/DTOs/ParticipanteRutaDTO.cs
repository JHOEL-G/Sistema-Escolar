using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ParticipanteRutaDTO
    {
        public int InscripcionRutaId { get; set; }
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? ApeLLido { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public decimal? Progreso { get; set; }
        public bool? Completado { get; set; }
        public decimal? CalificacionFinal { get; set; }
    }
}
