using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ActualizarPonderacionRequest
    {
        public List<PonderacionItemDTO> Ponderaciones { get; set; } = new();
        public decimal? Calificacion { get; set; }
        public int? RequisitoAvance { get; set; }
    }
}
