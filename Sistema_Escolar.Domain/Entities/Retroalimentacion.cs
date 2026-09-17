using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Retroalimentacion
{
    public int RetroId { get; set; }

    public string? NombreRetro { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
