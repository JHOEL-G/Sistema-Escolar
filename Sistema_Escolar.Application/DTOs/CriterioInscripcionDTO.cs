using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CriterioInscripcionDTO
    {
        public string? TipoCriterio { get; set; } = string.Empty;
        public int? OrganizationalUnitId { get; set; }
        public int? PropiedadId { get; set; }
        public string? ValorPropiedad { get; set; } = string.Empty;
        public string? OperadorLogico { get; set; } = "Y";
    }
}
