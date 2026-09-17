using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class GestionCurso
{
    public int GestionCursoId { get; set; }

    public string NombreCurso { get; set; } = null!;

    public string? Privacidad { get; set; }

    public bool? InscripcionAutomatica { get; set; }

    public bool? PermitirDesinscripcion { get; set; }

    public bool? Gamificacion { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? CreadoPor { get; set; }

    public int? CursoId { get; set; }

    public virtual Usuario? CreadoPorNavigation { get; set; }

    public virtual Curso? Curso { get; set; }

    public virtual ICollection<CursoCriteriosInscripcion> CursoCriteriosInscripcions { get; set; } = new List<CursoCriteriosInscripcion>();

    public virtual ICollection<CursoEvaluadore> CursoEvaluadores { get; set; } = new List<CursoEvaluadore>();

    public virtual ICollection<CursoParticipante> CursoParticipantes { get; set; } = new List<CursoParticipante>();

    public virtual ICollection<CursoTema> CursoTemas { get; set; } = new List<CursoTema>();

    public virtual ICollection<GestionCursoVisibilidad> GestionCursoVisibilidads { get; set; } = new List<GestionCursoVisibilidad>();
}
