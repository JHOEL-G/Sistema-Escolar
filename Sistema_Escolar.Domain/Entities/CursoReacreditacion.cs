using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CursoReacreditacion
{
    public int ReacreditacionId { get; set; }

    public int CursoId { get; set; }

    public int CursoReacreditacionId { get; set; }

    public int? PeriodoVigencia { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Curso CursoReacreditacionNavigation { get; set; } = null!;
}
