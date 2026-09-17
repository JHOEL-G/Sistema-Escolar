using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoEvaluacionPresencial
{
    public int EvaluacionPresencialId { get; set; }

    public int ModuloRecursoId { get; set; }

    public bool? AgregarPonderacion { get; set; }

    public bool? ColaboradorSolicitarRevision { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public int? TipoCalificacionId { get; set; }

    public string? NombreTitulo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EvaluacionPresencialCalificacion> EvaluacionPresencialCalificacions { get; set; } = new List<EvaluacionPresencialCalificacion>();

    public virtual ICollection<EvaluacionPresencialRubrica> EvaluacionPresencialRubricas { get; set; } = new List<EvaluacionPresencialRubrica>();

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual ICollection<Rubrica> Rubricas { get; set; } = new List<Rubrica>();

    public virtual TipoCalificacion? TipoCalificacion { get; set; }
}
