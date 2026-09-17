using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionPresencialCriterio
{
    public int CriterioId { get; set; }

    public int RubricaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual EvaluacionPresencialRubrica Rubrica { get; set; } = null!;
}
