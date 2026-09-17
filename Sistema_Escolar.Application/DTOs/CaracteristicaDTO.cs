using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CaracteristicaDTO
    {
        public int CaracteristicaId { get; set; }

        public int? CursoId { get; set; }

        public string? Tipo { get; set; }

        public string? Contenido { get; set; }
    }
}
