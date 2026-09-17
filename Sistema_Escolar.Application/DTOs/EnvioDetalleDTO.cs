using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class EnvioDetalleDTO
    {
        public int EnvioId { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int? UsuarioId { get; set; }
        public string TituloFormulario { get; set; } = string.Empty;
        public List<RespuestaDetalleDTO> Respuestas { get; set; } = new();
    }
}
