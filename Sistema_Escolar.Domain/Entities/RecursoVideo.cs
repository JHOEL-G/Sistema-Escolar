using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoVideo
{
    public int VideoId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? VideoPath { get; set; }

    public string? VideoLink { get; set; }

    public string? TipoSubida { get; set; }

    public bool? HacerVisibleDashboard { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;
}
