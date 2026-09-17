using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class UnidadesOrganizacionale
{
    public int OrganizacionalesId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? JefeId { get; set; }

    public virtual ICollection<ConfiguracionParticipante> ConfiguracionParticipantes { get; set; } = new List<ConfiguracionParticipante>();

    public virtual Jefedirecto? Jefe { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
