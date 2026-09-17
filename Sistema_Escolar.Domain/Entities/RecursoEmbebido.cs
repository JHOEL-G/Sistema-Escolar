using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoEmbebido
{
    public int EmbebidoId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? EnlaceEmbebido { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;
}
