using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class PuestoDTO
    {
        public int PuestoId { get; set; }
        public string Nombre_Puesto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
