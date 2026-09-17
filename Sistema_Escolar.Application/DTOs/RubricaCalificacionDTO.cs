using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RubricaCalificacionDTO
    {
        public int? CalificacionRubricaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Puntos { get; set; }
        public int OrdenCalificacion { get; set; }
    }
}
