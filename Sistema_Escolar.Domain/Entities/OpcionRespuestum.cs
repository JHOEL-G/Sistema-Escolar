using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class OpcionRespuestum
{
    public int OpcionId { get; set; }

    public int PreguntaId { get; set; }

    public string TextoOpcion { get; set; } = null!;

    public bool? EsCorrecta { get; set; }

    public int? Orden { get; set; }

    public string? ExplicacionOrelacion { get; set; }

    public virtual ICollection<EvaluacionRespuestum> EvaluacionRespuesta { get; set; } = new List<EvaluacionRespuestum>();

    public virtual Preguntum Pregunta { get; set; } = null!;
}
