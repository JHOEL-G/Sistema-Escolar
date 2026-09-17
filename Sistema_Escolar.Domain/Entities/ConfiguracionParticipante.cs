using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ConfiguracionParticipante
{
    public int ConfiguracionId { get; set; }

    public int? RolesId { get; set; }

    public int? UoId { get; set; }

    public int? PropiedadesId { get; set; }

    public bool? PermiteDesinscripcion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<ConfiguracionParticipantesDetalle> ConfiguracionParticipantesDetalles { get; set; } = new List<ConfiguracionParticipantesDetalle>();

    public virtual ConfiguracionPropiedade? Propiedades { get; set; }

    public virtual CatRole? Roles { get; set; }

    public virtual ICollection<RutaAprendizaje> RutaAprendizajes { get; set; } = new List<RutaAprendizaje>();

    public virtual UnidadesOrganizacionale? Uo { get; set; }
}
