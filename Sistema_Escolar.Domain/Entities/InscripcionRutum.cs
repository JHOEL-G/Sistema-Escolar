using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class InscripcionRutum
{
    public int InscripcionRutaId { get; set; }

    public int RutaId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public decimal? Progreso { get; set; }

    public bool? Completado { get; set; }

    public decimal? CalificacionFinal { get; set; }

    public bool? Activo { get; set; }

    public virtual RutaAprendizaje Ruta { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
