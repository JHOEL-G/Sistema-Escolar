using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Lenguaje
{
    public int LenguajeId { get; set; }

    public string? NombreLenguaje { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
