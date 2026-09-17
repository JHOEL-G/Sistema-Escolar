using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RubricaDTO
    {
        public int? RubricaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Visible { get; set; } = true;
        public List<RubricaCriterioDTO>? Criterios { get; set; }
    }
}
