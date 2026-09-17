using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class UsuarioValor
{
    public int UsuarioId { get; set; }

    public int PropiedadId { get; set; }

    public string? Valor { get; set; }

    public virtual ConfiguracionPropiedade Propiedad { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
