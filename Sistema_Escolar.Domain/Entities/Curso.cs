using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Curso
{
    public int CursoId { get; set; }

    public string? NombreCurso { get; set; }

    public bool? HhabilitarFechaCurso { get; set; }

    public string? ImagenPortadaPath { get; set; }

    public int? DificultadId { get; set; }

    public int? LenguajeId { get; set; }

    public string? DescripcionCurso { get; set; }

    public string? CaracterísticasQueAprendere { get; set; }

    public string? CaracterísticasHabilidades { get; set; }

    public string? CaracterísticasRequerimientos { get; set; }

    public string? VideoPromocionalPath { get; set; }

    public string? Avance { get; set; }

    public int? RetroalimentacionId { get; set; }

    public string? DuracionCurso { get; set; }

    public string? MensajeBienvenida { get; set; }

    public int? InstructorId { get; set; }

    public bool? Reacreditación { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public bool? EstaPublicado { get; set; }

    public string? PorQueInscribirmeCurso { get; set; }

    public int? Calificacion { get; set; }

    public virtual ICollection<CursoInstructor> CursoInstructors { get; set; } = new List<CursoInstructor>();

    public virtual ICollection<CursoReacreditacion> CursoReacreditacionCursoReacreditacionNavigations { get; set; } = new List<CursoReacreditacion>();

    public virtual ICollection<CursoReacreditacion> CursoReacreditacionCursos { get; set; } = new List<CursoReacreditacion>();

    public virtual Dificultad? Dificultad { get; set; }

    public virtual ICollection<GestionCurso> GestionCursos { get; set; } = new List<GestionCurso>();

    public virtual Usuario? Instructor { get; set; }

    public virtual Lenguaje? Lenguaje { get; set; }

    public virtual ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();

    public virtual Retroalimentacion? Retroalimentacion { get; set; }

    public virtual ICollection<UsuarioCurso> UsuarioCursos { get; set; } = new List<UsuarioCurso>();
}
