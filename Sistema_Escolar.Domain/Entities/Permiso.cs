using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Permiso
{
    public int PermisoId { get; set; }

    public string? NombrePermiso { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
