using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CalificacionPresencialDTO
    {
        public int RubricaOrden { get; set; }
        public int CriterioOrden { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Puntos { get; set; }
        public int OrdenCalificacion { get; set; }
    }
}
