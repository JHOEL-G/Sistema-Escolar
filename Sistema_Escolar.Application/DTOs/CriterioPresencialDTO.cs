using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CriterioPresencialDTO
    {
        public int RubricaOrden { get; set; }
        public string TituloCriterio { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int OrdenCriterio { get; set; }
    }
}
