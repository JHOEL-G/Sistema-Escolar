using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class LecturaArchivoAdjunto
{
    public int ArchivoAdjuntoId { get; set; }

    public int LecturaId { get; set; }

    public string? NombreArchivo { get; set; }

    public string? RutaArchivo { get; set; }

    public string? TipoArchivo { get; set; }

    public decimal? TamañoMb { get; set; }

    public DateTime? FechaSubida { get; set; }

    public virtual RecursoLectura Lectura { get; set; } = null!;
}
