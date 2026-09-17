using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoLecturaDTO
    {
        public int? LecturaId { get; set; }
        public int ModuloRecursoId { get; set; }
        public int? TipoLecturaId { get; set; } 
        public string? Descripcion { get; set; }
        public string? ContenidoHTML { get; set; }
        public string? ArchivoPDFPath { get; set; }
        public string? NombreArchivoPDF { get; set; }
        public bool? HacerVisibleDashboard { get; set; }
        public List<ArchivoAdjuntoDTO>? ArchivosAdjuntos { get; set; }
    }
}
