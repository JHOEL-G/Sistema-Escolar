using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoTareaDTO
    {
        public int? TareaId { get; set; }
        public int ModuloRecursoId { get; set; }
        public string? Instrucciones { get; set; }
        public string? Descripcion { get; set; }
        public bool? AgregarPonderacion { get; set; }
        public string Privacidad { get; set; } = "Privado";
        public int TipoCalificacionId { get; set; } = 1;
        public string? ArchivoPath { get; set; }

        public List<ArchivoDTO>? Archivos { get; set; }
    }
}
