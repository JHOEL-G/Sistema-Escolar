using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Tema
{
    public int TemaId { get; set; }

    public string? NombreTema { get; set; }

    public string? Descripcion { get; set; }

    public string? ImagenPortada { get; set; }

    public string? CreacionSubtema { get; set; }

    public virtual ICollection<CursoTema> CursoTemas { get; set; } = new List<CursoTema>();
}
