using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CalificacionRecursoDTO
    {
        public int TipoRecurso { get; set; }
        public int RecursoId { get; set; }
        public int UsuarioId { get; set; }
        public int CursoId { get; set; }
        public decimal Calificacion { get; set; }
        public string? Comentario { get; set; }
        public int? CalificadoPorId { get; set; }
    }
}
