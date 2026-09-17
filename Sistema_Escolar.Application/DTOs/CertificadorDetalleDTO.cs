using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CertificadorDetalleDTO
    {
        public int CertificadorRutaId { get; set; }
        public int UsuarioId { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Area { get; set; }
        public string? Iniciales { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public bool Activo { get; set; }
    }
}
