using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class OuDTO
    {
        public int OrganizacionalesId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int? JefeId { get; set; }
        public int? CantidadColaboradores { get; set; }
    }
}
