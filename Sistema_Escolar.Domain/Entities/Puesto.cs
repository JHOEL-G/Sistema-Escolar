using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Puesto
{
    public int PuestoId { get; set; }

    public string NombrePuesto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
