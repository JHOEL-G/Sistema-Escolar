using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CambiarEstadoCursoRequest
    {
        public int AdminId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? Motivo { get; set; }
    }
}
