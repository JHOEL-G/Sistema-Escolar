using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionPresencialCalificacion
{
    public int EvaluacionCalificacionId { get; set; }

    public int EvaluacionPresencialId { get; set; }

    public int UsuarioId { get; set; }

    public int CriterioId { get; set; }

    public int CalificacionRubricaId { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public int? CalificadoPorId { get; set; }

    public virtual RubricaCalificacion CalificacionRubrica { get; set; } = null!;

    public virtual Usuario? CalificadoPor { get; set; }

    public virtual RubricaCriterio Criterio { get; set; } = null!;

    public virtual RecursoEvaluacionPresencial EvaluacionPresencial { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
