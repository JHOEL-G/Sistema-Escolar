using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionPresencialRubrica
{
    public int RubricaId { get; set; }

    public int EvaluacionPresencialId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual RecursoEvaluacionPresencial EvaluacionPresencial { get; set; } = null!;

    public virtual ICollection<EvaluacionPresencialCriterio> EvaluacionPresencialCriterios { get; set; } = new List<EvaluacionPresencialCriterio>();
}
