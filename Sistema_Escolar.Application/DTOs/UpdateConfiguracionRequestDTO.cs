using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class UpdateConfiguracionRequestDTO
    {
        public string KeyName { get; set; } = string.Empty;
        public bool NuevoEstado { get; set; }
    }
}
