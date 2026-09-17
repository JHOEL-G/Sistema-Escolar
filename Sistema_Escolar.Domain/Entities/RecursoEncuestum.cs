using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoEncuestum
{
    public int EncuestaId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? Instrucciones { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EncuestaBanca> EncuestaBancas { get; set; } = new List<EncuestaBanca>();

    public virtual ICollection<EncuestaPreguntum> EncuestaPregunta { get; set; } = new List<EncuestaPreguntum>();

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;
}
