using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ForoPublicacion
{
    public int PublicacionId { get; set; }

    public int ForoId { get; set; }

    public int UsuarioId { get; set; }

    public string? Contenido { get; set; }

    public int? PublicacionPadreId { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public bool? Activo { get; set; }

    public string? ArchivoPath { get; set; }

    public virtual RecursoForo Foro { get; set; } = null!;

    public virtual ICollection<ForoPublicacion> InversePublicacionPadre { get; set; } = new List<ForoPublicacion>();

    public virtual ForoPublicacion? PublicacionPadre { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
