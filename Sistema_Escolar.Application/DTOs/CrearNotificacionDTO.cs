using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CrearNotificacionDTO
    {
        public int UsuarioId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public int? ReferenciaId { get; set; }
    }
}
