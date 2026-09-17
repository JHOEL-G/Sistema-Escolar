using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PreguntaFormularioDTO
    {
        public string TipoPregunta { get; set; } = string.Empty;
        public string Etiqueta { get; set; } = string.Empty;
        public bool Obligatorio { get; set; }
        public int Orden { get; set; }
        public List<OpcionFormularioDTO> Opciones { get; set; } = new();
    }
}
