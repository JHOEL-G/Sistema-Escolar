using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class EliminarCursoRequest
    {
        public int AdminId { get; set; }
        public string? Motivo { get; set; }
    }
}
