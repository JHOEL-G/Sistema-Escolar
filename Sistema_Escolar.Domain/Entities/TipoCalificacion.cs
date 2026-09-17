using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TipoCalificacion
{
    public int CalificaionId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<RecursoEvaluacionPresencial> RecursoEvaluacionPresencials { get; set; } = new List<RecursoEvaluacionPresencial>();

    public virtual ICollection<RecursoEvaluacion> RecursoEvaluacions { get; set; } = new List<RecursoEvaluacion>();

    public virtual ICollection<RecursoForo> RecursoForos { get; set; } = new List<RecursoForo>();

    public virtual ICollection<RecursoScorm> RecursoScorms { get; set; } = new List<RecursoScorm>();

    public virtual ICollection<RecursoTarea> RecursoTareas { get; set; } = new List<RecursoTarea>();
}
