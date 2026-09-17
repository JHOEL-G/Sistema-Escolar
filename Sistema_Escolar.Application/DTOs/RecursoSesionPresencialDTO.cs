using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RecursoSesionPresencialDTO
    {
        public int? SesionPresencialId { get; set; }
        public int ModuloRecursoId { get; set; }
        public DateTime FechaSesion { get; set; }
        public string Lugar { get; set; } = string.Empty;
        public int Duracion { get; set; }
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; }
        public string? Instrucciones { get; set; }
        public TimeSpan? HoraFin { get; set; }

    }
}
