using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class FormularioEnvio
{
    public int EnvioId { get; set; }

    public int PlantillaId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public virtual ICollection<FormularioRespuestum> FormularioRespuesta { get; set; } = new List<FormularioRespuestum>();

    public virtual FormularioPlantilla Plantilla { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
