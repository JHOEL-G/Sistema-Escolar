using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class DocumentoRole
{
    public int Id { get; set; }

    public int GestionId { get; set; }

    public int RolId { get; set; }

    public virtual GestionDocumeto Gestion { get; set; } = null!;

    public virtual CatRole Rol { get; set; } = null!;
}
