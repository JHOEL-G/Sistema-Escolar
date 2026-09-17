using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Jefedirecto
{
    public int JefeId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<UnidadesOrganizacionale> UnidadesOrganizacionales { get; set; } = new List<UnidadesOrganizacionale>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
