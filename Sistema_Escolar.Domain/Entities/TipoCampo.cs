using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TipoCampo
{
    public int CampoId { get; set; }

    public string? NombreCampo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<ConfiguracionPropiedade> ConfiguracionPropiedades { get; set; } = new List<ConfiguracionPropiedade>();
}
