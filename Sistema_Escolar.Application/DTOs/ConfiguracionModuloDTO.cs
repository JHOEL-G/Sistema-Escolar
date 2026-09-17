using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ConfiguracionModuloDTO
    {
        public string KeyName { get; set; } = string.Empty;
        public bool Estado { get; set; }

        public string? Categoria { get; set; }
        public string? Label { get; set; }
        public string? Descripcion { get; set; }
        public bool EsMaestro { get; set; }
        public bool EsNuevo { get; set; }
    }
}
