using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoScorm
{
    public int ScormId { get; set; }

    public int ModuloRecursoId { get; set; }

    public string? ArchivoPath { get; set; }

    public string? NombreArchivo { get; set; }

    public decimal? TamañoMb { get; set; }

    public bool? AgregarPonderacion { get; set; }

    public bool? PermitirModoPantallaCompleta { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public int? TipoCalificacionId { get; set; }

    public string? Descripcion { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual TipoCalificacion? TipoCalificacion { get; set; }
}
