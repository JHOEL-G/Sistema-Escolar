using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TipoPreguntum
{
    public int TipoPreguntaId { get; set; }

    public string? NombreTipo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
