using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RecursoSesionPresencial
{
    public int SesionPresencialId { get; set; }

    public int ModuloRecursoId { get; set; }

    public DateTime? FechaSesion { get; set; }

    public string? Lugar { get; set; }

    public int? Duracion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public string? Descripcion { get; set; }

    public string? Direccion { get; set; }

    public string? Instrucciones { get; set; }

    public TimeOnly? HoraFin { get; set; }

    public virtual ModuloRecurso ModuloRecurso { get; set; } = null!;
}
