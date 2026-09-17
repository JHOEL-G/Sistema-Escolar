using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CatRolDTO
    {
        public int RolId { get; set; }
        public string? NombreRol { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
        public int? CantidadUsuarios { get; set; }
    }
}
