using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoTarea
{
    public int TareaId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? Instrucciones { get; set; }

    public bool? AgregarPonderacion { get; set; }

    public string? Privacidad { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public int? TipoCalificacionId { get; set; }

    public string? ArchivoPath { get; set; }

    public string? Descripcion { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual ICollection<TareaEntrega> TareaEntregas { get; set; } = new List<TareaEntrega>();

    public virtual TipoCalificacion? TipoCalificacion { get; set; }
}
