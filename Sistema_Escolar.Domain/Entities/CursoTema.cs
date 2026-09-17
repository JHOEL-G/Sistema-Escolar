using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CursoTema
{
    public int CursoTemaId { get; set; }

    public int? GestionCursoId { get; set; }

    public int? TemaId { get; set; }

    public int? Orden { get; set; }

    public virtual GestionCurso? GestionCurso { get; set; }

    public virtual Tema? Tema { get; set; }
}
