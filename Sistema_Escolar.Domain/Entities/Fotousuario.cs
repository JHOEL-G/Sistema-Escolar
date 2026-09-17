using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Fotousuario
{
    public int FotoId { get; set; }

    public int UsuarioId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public DateTime? FechaSubida { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
