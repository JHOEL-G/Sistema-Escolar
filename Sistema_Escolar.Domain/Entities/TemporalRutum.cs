using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TemporalRutum
{
    public int TemporalId { get; set; }

    public DateTime? AsignarFecha { get; set; }

    public string? AsignarDia { get; set; }

    public bool? FechaLimite { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<RutaAprendizaje> RutaAprendizajes { get; set; } = new List<RutaAprendizaje>();
}
