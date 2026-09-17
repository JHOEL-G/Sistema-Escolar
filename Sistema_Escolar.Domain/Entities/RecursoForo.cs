using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoForo
{
    public int ForoId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? NombreForo { get; set; }

    public string? Instrucciones { get; set; }

    public bool? AgregarPonderacion { get; set; }

    public string? Privacidad { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public int? TipoCalificacionId { get; set; }

    public string? ArchivoPath { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<ForoCalificacion> ForoCalificacions { get; set; } = new List<ForoCalificacion>();

    public virtual ICollection<ForoPublicacion> ForoPublicacions { get; set; } = new List<ForoPublicacion>();

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual TipoCalificacion? TipoCalificacion { get; set; }
}
