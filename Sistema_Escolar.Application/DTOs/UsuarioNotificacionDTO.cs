using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class UsuarioNotificacionDTO
    {
        public int UsuarioId { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
