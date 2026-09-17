using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Preguntum
{
    public int PreguntaId { get; set; }

    public int BancaId { get; set; }

    public int TipoPreguntaId { get; set; }

    public string TextoPregunta { get; set; } = null!;

    public string? Explicacion { get; set; }

    public decimal? PuntosValor { get; set; }

    public int? Orden { get; set; }

    public bool? Activo { get; set; }

    public virtual BancaPregunta Banca { get; set; } = null!;

    public virtual ICollection<EvaluacionRespuestum> EvaluacionRespuesta { get; set; } = new List<EvaluacionRespuestum>();

    public virtual ICollection<OpcionRespuestum> OpcionRespuesta { get; set; } = new List<OpcionRespuestum>();

    public virtual TipoPreguntum TipoPregunta { get; set; } = null!;
}
