using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CrearFormularioDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public List<PreguntaFormularioDTO> Preguntas { get; set; } = new();
    }
}
