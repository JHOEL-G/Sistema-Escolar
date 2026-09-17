using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionPreguntaOpcion
{
    public int OpcionId { get; set; }

    public int EvaluacionPreguntaId { get; set; }

    public string TextoOpcion { get; set; } = null!;

    public bool? EsCorrecta { get; set; }

    public int? Orden { get; set; }

    public string? ExplicacionOrelacion { get; set; }

    public string? ImagenOpcion { get; set; }

    public virtual EvaluacionPreguntum EvaluacionPregunta { get; set; } = null!;
}
