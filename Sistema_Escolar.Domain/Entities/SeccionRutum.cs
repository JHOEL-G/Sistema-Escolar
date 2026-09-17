using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class SeccionRutum
{
    public int SeccionId { get; set; }

    public int RutaId { get; set; }

    public string NombreSeccion { get; set; } = null!;

    public int Orden { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual RutaAprendizaje Ruta { get; set; } = null!;

    public virtual ICollection<RutaCurso> RutaCursos { get; set; } = new List<RutaCurso>();

    public virtual ICollection<SeccionCurso> SeccionCursos { get; set; } = new List<SeccionCurso>();
}
