using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Modulo
{
    public int ModuloId { get; set; }

    public int CursoId { get; set; }

    public string? ModuloTitulo { get; set; }

    public string? Descripcion { get; set; }

    public int? OrdenModulo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual ICollection<ModuloRecurso> ModuloRecursos { get; set; } = new List<ModuloRecurso>();
}
