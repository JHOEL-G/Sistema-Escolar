using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class GestionCursoVisibilidad
{
    public int VisibilidadId { get; set; }

    public int GestionCursoId { get; set; }

    public string TipoGrupo { get; set; } = null!;

    public int GrupoId { get; set; }

    public virtual GestionCurso GestionCurso { get; set; } = null!;
}
