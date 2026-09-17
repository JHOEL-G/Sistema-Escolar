using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RutaCurso
{
    public int RutaCursoId { get; set; }

    public int RutaId { get; set; }

    public int? SeccionId { get; set; }

    public int CursoId { get; set; }

    public int Orden { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public bool? Activo { get; set; }

    public virtual RutaAprendizaje Ruta { get; set; } = null!;

    public virtual SeccionRutum? Seccion { get; set; }
}
