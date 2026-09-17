using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class SistemaConfiguracion
{
    public int ConfigId { get; set; }

    public string KeyName { get; set; } = null!;

    public string Label { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Categoria { get; set; } = null!;

    public bool? Estado { get; set; }

    public bool? EsMaestro { get; set; }

    public bool? EsNuevo { get; set; }

    public DateTime? FechaModificacion { get; set; }
}
