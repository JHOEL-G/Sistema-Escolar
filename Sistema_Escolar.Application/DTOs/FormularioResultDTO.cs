using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class FormularioResultDTO
    {
        public int ResultId { get; set; }
        public Guid? PublicId { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
