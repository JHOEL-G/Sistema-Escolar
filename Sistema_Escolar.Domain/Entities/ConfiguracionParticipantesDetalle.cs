using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ConfiguracionParticipantesDetalle
{
    public int DetalleId { get; set; }

    public int ConfigId { get; set; }

    public int EntidadId { get; set; }

    public string TipoEntidad { get; set; } = null!;

    public virtual ConfiguracionParticipante Config { get; set; } = null!;
}
