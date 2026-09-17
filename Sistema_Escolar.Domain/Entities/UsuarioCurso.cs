using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class UsuarioCurso
{
    public int InscripcionId { get; set; }

    public int UsuarioId { get; set; }

    public int CursoId { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public decimal? Progreso { get; set; }

    public decimal? CalificacionFinal { get; set; }

    public bool? EsCompletado { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
