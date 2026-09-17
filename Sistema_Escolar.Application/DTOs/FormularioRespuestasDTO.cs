using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class FormularioRespuestasDTO
    {
        public List<EnvioDetalleDTO> Envios { get; set; } = new();
    }
}
