using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionRespuestum
{
    public int RespuestaId { get; set; }

    public int EvaluacionId { get; set; }

    public int UsuarioId { get; set; }

    public int PreguntaId { get; set; }

    public int? OpcionId { get; set; }

    public bool? EsCorrecta { get; set; }

    public decimal? PuntosObtenidos { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public int? Intento { get; set; }

    public virtual RecursoEvaluacion Evaluacion { get; set; } = null!;

    public virtual OpcionRespuestum? Opcion { get; set; }

    public virtual Preguntum Pregunta { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
