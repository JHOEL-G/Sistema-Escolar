using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class FormularioPlantilla
{
    public int PlantillaId { get; set; }

    public Guid PublicId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Publicado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<FormularioEnvio> FormularioEnvios { get; set; } = new List<FormularioEnvio>();

    public virtual ICollection<FormularioPreguntum> FormularioPregunta { get; set; } = new List<FormularioPreguntum>();
}
