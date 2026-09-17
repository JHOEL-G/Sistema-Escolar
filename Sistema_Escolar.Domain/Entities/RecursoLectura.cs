using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoLectura
{
    public int LecturaId { get; set; }

    public int ModuloRecursoId { get; set; }

    public int TipoLecturaId { get; set; }

    public string? Descripcion { get; set; }

    public string? ContenidoHtml { get; set; }

    public string? ArchivoPdfpath { get; set; }

    public string? NombreArchivoPdf { get; set; }

    public bool? HacerVisibleDashboard { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<LecturaArchivoAdjunto> LecturaArchivoAdjuntos { get; set; } = new List<LecturaArchivoAdjunto>();

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;

    public virtual TipoLectura TipoLectura { get; set; } = null!;
}
