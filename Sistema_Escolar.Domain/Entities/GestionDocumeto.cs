using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class GestionDocumeto
{
    public int GestionId { get; set; }

    public string TituloDocumento { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? DocumentoPath { get; set; }

    public int? FormatoPermitidoId { get; set; }

    public int? RolesAccesoId { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaExpiracion { get; set; }

    public int? ExploradorId { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DocumentoFormato> DocumentoFormatos { get; set; } = new List<DocumentoFormato>();

    public virtual ICollection<DocumentoRole> DocumentoRoles { get; set; } = new List<DocumentoRole>();

    public virtual ExploradorArchivo? Explorador { get; set; }

    public virtual Formato? FormatoPermitido { get; set; }

    public virtual CatRole? RolesAcceso { get; set; }
}
