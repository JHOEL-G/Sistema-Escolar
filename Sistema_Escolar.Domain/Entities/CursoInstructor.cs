using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CursoInstructor
{
    public int CursoInstructorId { get; set; }

    public int CursoId { get; set; }

    public int InstructorId { get; set; }

    public bool? EsInstructorPrincipal { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Usuario Instructor { get; set; } = null!;
}
