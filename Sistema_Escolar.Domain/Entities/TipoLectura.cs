using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TipoLectura
{
    public int TipoLecturaId { get; set; }

    public string? NombreTipo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<RecursoLectura> RecursoLecturas { get; set; } = new List<RecursoLectura>();
}
