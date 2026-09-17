using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CursoEvaluadore
{
    public int CursoEvaluadorId { get; set; }

    public int? GestionCursoId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual GestionCurso? GestionCurso { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
