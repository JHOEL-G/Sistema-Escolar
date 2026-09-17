using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class EnvioFormularioDTO
    {
        public int PlantillaId { get; set; }
        public int? UsuarioId { get; set; }
        public List<RespuestaFormularioDTO> Respuestas { get; set; } = new();
    }
}
