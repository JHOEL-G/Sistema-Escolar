using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RubricaCriterio
{
    public int CriterioId { get; set; }

    public int RubricaId { get; set; }

    public string? TituloCriterio { get; set; }

    public string? Descripcion { get; set; }

    public int? OrdenCriterio { get; set; }

    public virtual ICollection<EvaluacionPresencialCalificacion> EvaluacionPresencialCalificacions { get; set; } = new List<EvaluacionPresencialCalificacion>();

    public virtual Rubrica Rubrica { get; set; } = null!;

    public virtual ICollection<RubricaCalificacion> RubricaCalificacions { get; set; } = new List<RubricaCalificacion>();
}
