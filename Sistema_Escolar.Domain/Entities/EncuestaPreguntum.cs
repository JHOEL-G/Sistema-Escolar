using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EncuestaPreguntum
{
    public int EncuestaPreguntaId { get; set; }

    public int EncuestaId { get; set; }

    public string? TextoPregunta { get; set; }

    public string? TipoPregunta { get; set; }

    public int? OrdenPregunta { get; set; }

    public int? LimiteRespuestas { get; set; }

    public virtual RecursoEncuestum Encuesta { get; set; } = null!;

    public virtual ICollection<EncuestaOpcion> EncuestaOpcions { get; set; } = new List<EncuestaOpcion>();

    public virtual ICollection<EncuestaRespuestum> EncuestaRespuesta { get; set; } = new List<EncuestaRespuestum>();
}
