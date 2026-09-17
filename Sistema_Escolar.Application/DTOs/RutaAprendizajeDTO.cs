using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RutaAprendizajeDTO
    {
        public int RutaId { get; set; }
        public string? NombreRuta { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
        public string? ImagenPortada { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; }

        public string? NombrePrivacidad { get; set; } = string.Empty;
        public bool? Externo { get; set; }
        public List<int>? UOsPrivacidad { get; set; }
        public List<SeleccionadoDTO>? SeleccionadosPrivacidad { get; set; }

        public DateTime? AsignarFecha { get; set; }
        public string? AsignarDia { get; set; } = string.Empty;
        public bool? FechaLimite { get; set; }

        public List<SeccionDTO>? Secciones { get; set; }

        public int? PropiedadesId { get; set; }
        public bool? PermiteDesinscripcion { get; set; }
        public List<SeleccionadoDTO>? InscripcionAutomatica { get; set; } 

        public string? NombreCertificado { get; set; } = string.Empty;
        public bool? CondicionAvanceCurso { get; set; }
        public bool? CondicionAvanceSeccion { get; set; }
        public string? CriterioAprobacion { get; set; } = string.Empty;
        public string? Gamificacion { get; set; } = string.Empty;
        public string? MensajeBienvenida { get; set; } = string.Empty;

        public List<int>? Participantes { get; set; }
        public List<int>? Certificadores { get; set; }
    }
}
