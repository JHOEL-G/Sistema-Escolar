using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ExploradorArchivo
{
    public int ExploradorId { get; set; }

    public string NombreCarpeta { get; set; } = null!;

    public int? PadreId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<GestionDocumeto> GestionDocumetos { get; set; } = new List<GestionDocumeto>();

    public virtual ICollection<ExploradorArchivo> InversePadre { get; set; } = new List<ExploradorArchivo>();

    public virtual ExploradorArchivo? Padre { get; set; }
}
