using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class UsuarioRecursoProgreso
{
    public int ProgresoId { get; set; }

    public int UsuarioId { get; set; }

    public int ModuloRecursoId { get; set; }

    public DateTime? FechaCompletado { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
