using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoCalificacionDTO
    {
        public int UsuarioId { get; set; }
        public int ModuloRecursoId { get; set; }
        public int Ponderacion { get; set; }
        public int TipoRecurso { get; set; }
        public int RecursoId { get; set; }
        public string NombreRecurso { get; set; } = string.Empty;
        public decimal? Calificacion { get; set; }
        public bool EsPresencial { get; set; }
        public bool EsScorm { get; set; }
        public bool TieneAsistencia { get; set; }
        public string NombreTipoRecurso { get; set; } = "—";

    }
}
