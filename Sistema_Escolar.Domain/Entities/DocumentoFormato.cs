using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class DocumentoFormato
{
    public int Id { get; set; }

    public int GestionId { get; set; }

    public int FormatoId { get; set; }

    public virtual Formato Formato { get; set; } = null!;

    public virtual GestionDocumeto Gestion { get; set; } = null!;
}
