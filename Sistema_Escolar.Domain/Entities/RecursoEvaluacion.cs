using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoEvaluacion
{
    public int EvaluacionId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? Instrucciones { get; set; }

    public bool? AgregarPonderacion { get; set; }

    public bool? PreguntasAleatorias { get; set; }

    public int? Oportunidades { get; set; }

    public string? PermitirReinicio { get; set; }

    public int? PreguntasCorrectasAprobar { get; set; }

    public int? TiempoHoras { get; set; }

    public int? TiempoMinutos { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public string? NombreEvaluacion { get; set; }

    public int? TipoCalificacionId { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EvaluacionBanca> EvaluacionBancas { get; set; } = new List<EvaluacionBanca>();

    public virtual ICollection<EvaluacionPreguntum> EvaluacionPregunta { get; set; } = new List<EvaluacionPreguntum>();

    public virtual ICollection<EvaluacionRespuestum> EvaluacionRespuesta { get; set; } = new List<EvaluacionRespuestum>();

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual TipoCalificacion? TipoCalificacion { get; set; }
}
