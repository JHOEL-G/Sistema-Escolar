using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoEvaluacionPresencialDTO
    {
        public int? EvaluacionPresencialId { get; set; }
        public int ModuloRecursoId { get; set; }
        public bool AgregarPonderacion { get; set; }
        public bool ColaboradorSolicitarRevision { get; set; }
        public int TipoCalificacionId { get; set; } = 1;
        public string? Descripcion { get; set; }                       
        public List<RubricaPresencialDTO>? Rubricas { get; set; }       
        public List<CriterioPresencialDTO>? Criterios { get; set; }     
        public List<CalificacionPresencialDTO>? Calificaciones { get; set; }
    }
}
