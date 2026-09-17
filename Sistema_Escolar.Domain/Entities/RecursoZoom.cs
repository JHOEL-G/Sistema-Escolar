using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoZoom
{
    public int ZoomId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? EnlaceZoom { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;
}
