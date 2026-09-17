using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RubricaCalificacion
{
    public int CalificacionRubricaId { get; set; }

    public int CriterioId { get; set; }

    public string? Nombre { get; set; }

    public decimal? Puntos { get; set; }

    public int? OrdenCalificacion { get; set; }

    public virtual RubricaCriterio Criterio { get; set; } = null!;

    public virtual ICollection<EvaluacionPresencialCalificacion> EvaluacionPresencialCalificacions { get; set; } = new List<EvaluacionPresencialCalificacion>();
}
