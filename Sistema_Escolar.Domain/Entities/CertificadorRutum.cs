using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CertificadorRutum
{
    public int CertificadorRutaId { get; set; }

    public int RutaId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public bool Activo { get; set; }

    public virtual RutaAprendizaje Ruta { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
