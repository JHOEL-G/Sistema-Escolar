using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TipoRecurso
{
    public int RecursoId { get; set; }

    public string NombreTipo { get; set; } = null!;

    public string? Categoria { get; set; }

    public string? Icono { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<ModuloRecurso> ModuloRecursos { get; set; } = new List<ModuloRecurso>();
}
