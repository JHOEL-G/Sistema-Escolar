using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class SeccionCurso
{
    public int SeccionCursoId { get; set; }

    public int SeccionId { get; set; }

    public int CursoId { get; set; }

    public int Orden { get; set; }

    public virtual SeccionRutum Seccion { get; set; } = null!;
}
