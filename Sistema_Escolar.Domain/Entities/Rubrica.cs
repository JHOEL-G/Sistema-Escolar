using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Rubrica
{
    public int RubricaId { get; set; }

    public int? EvaluacionPresencialId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Visible { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual RecursoEvaluacionPresencial? EvaluacionPresencial { get; set; }

    public virtual ICollection<RubricaCriterio> RubricaCriterios { get; set; } = new List<RubricaCriterio>();
}
