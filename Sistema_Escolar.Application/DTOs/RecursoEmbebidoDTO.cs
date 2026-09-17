using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoEmbebidoDTO
    {
        public int? EmbebidoId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string EnlaceEmbebido { get; set; } = string.Empty;
    }
}
