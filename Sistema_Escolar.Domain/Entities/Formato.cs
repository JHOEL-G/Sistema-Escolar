using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Formato
{
    public int FormatoId { get; set; }

    public string NombreFormato { get; set; } = null!;

    public virtual ICollection<DocumentoFormato> DocumentoFormatos { get; set; } = new List<DocumentoFormato>();

    public virtual ICollection<GestionDocumeto> GestionDocumetos { get; set; } = new List<GestionDocumeto>();
}
