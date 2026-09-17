using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Notificacion
{
    public int NotificacionId { get; set; }

    public int UsuarioId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public int? ReferenciaId { get; set; }

    public bool Leida { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
