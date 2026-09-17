using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoDTO
    {
        public int ModuloId { get; set; }
        public int RecursoId { get; set; }
        public int OrdenRecurso { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public int ModuloRecursoId { get; set; }
        public string NombreTipo { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public int? Ponderacion { get; set; }

        public JsonElement? DataJson { get; set; }
    }
}
