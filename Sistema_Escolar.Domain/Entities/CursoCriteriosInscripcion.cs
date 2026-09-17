using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CursoCriteriosInscripcion
{
    public int CriterioId { get; set; }

    public int? GestionCursoId { get; set; }

    public string TipoCriterio { get; set; } = null!;

    public int? OrganizationalUnitId { get; set; }

    public int? PropiedadId { get; set; }

    public string? ValorPropiedad { get; set; }

    public string? OperadorLogico { get; set; }

    public virtual GestionCurso? GestionCurso { get; set; }
}
