using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CalificacionResumenDTO
    {
        public int Modulo { get; set; }
        public string Leccion { get; set; } = string.Empty;
        public string TipoRecurso { get; set; } = string.Empty;
        public int TipoRecursoId { get; set; }
        public int RecursoModuloId { get; set; }
        public int RecursoEspecificoId { get; set; }
        public int TipoCalificacionId { get; set; }
        public int ParticipacionesTotales { get; set; }
        public int Pendientes { get; set; }
        public int Calificados { get; set; }
    }
}
