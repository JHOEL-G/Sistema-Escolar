using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class PrivacidadUo
{
    public int PrivacidadUoId { get; set; }

    public int PrivacidadId { get; set; }

    public int EntidadId { get; set; }

    public string TipoEntidad { get; set; } = null!;

    public virtual Privacidad Privacidad { get; set; } = null!;
}
