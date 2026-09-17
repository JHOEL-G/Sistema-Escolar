using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoScormDTO
    {
        public int? ScormId { get; set; }
        public int? ModuloRecursoId { get; set; }
        public string? ArchivoPath { get; set; }
        public string? NombreArchivo { get; set; }
        public decimal? TamañoMB { get; set; }
        public bool? AgregarPonderacion { get; set; }
        public bool? PermitirModoPantallaCompleta { get; set; } = true;
        public int? TipoCalificacionId { get; set; } = 1;
        public string? Descripcion { get; set; }
    }
}
