using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Privacidad
{
    public int PrivacidadId { get; set; }

    public string? NombrePrivacidad { get; set; }

    public bool? Activo { get; set; }

    public bool? Externo { get; set; }

    public virtual ICollection<PrivacidadUo> PrivacidadUos { get; set; } = new List<PrivacidadUo>();

    public virtual ICollection<RutaAprendizaje> RutaAprendizajes { get; set; } = new List<RutaAprendizaje>();
}
