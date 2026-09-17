using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CatRole
{
    public int RolId { get; set; }

    public string NombreRol { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<ConfiguracionParticipante> ConfiguracionParticipantes { get; set; } = new List<ConfiguracionParticipante>();

    public virtual ICollection<DocumentoRole> DocumentoRoles { get; set; } = new List<DocumentoRole>();

    public virtual ICollection<GestionDocumeto> GestionDocumetos { get; set; } = new List<GestionDocumeto>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
