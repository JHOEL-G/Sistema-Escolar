using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Dificultad
{
    public int DificultadId { get; set; }

    public string? NombreDificultad { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
