using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionPreguntum
{
    public int EvaluacionPreguntaId { get; set; }

    public int EvaluacionId { get; set; }

    public int TipoPreguntaId { get; set; }

    public string TextoPregunta { get; set; } = null!;

    public decimal? PuntosValor { get; set; }

    public int? Orden { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? ImagenPregunta { get; set; }

    public virtual RecursoEvaluacion Evaluacion { get; set; } = null!;

    public virtual ICollection<EvaluacionPreguntaOpcion> EvaluacionPreguntaOpcions { get; set; } = new List<EvaluacionPreguntaOpcion>();
}
