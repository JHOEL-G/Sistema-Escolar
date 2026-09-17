using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RubricaCriterioDTO
    {
        public int? CriterioId { get; set; }
        public string TituloCriterio { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int OrdenCriterio { get; set; }
        public List<RubricaCalificacionDTO>? Calificaciones { get; set; }
    }
}
