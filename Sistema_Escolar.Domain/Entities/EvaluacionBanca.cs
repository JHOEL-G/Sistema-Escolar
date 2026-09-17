using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionBanca
{
    public int EvaluacionBancaId { get; set; }

    public int? EvaluacionId { get; set; }

    public int? BancaId { get; set; }

    public virtual BancaPregunta? Banca { get; set; }

    public virtual RecursoEvaluacion? Evaluacion { get; set; }
}
