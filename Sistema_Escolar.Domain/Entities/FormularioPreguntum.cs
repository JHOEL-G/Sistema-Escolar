using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class FormularioPreguntum
{
    public int PreguntaId { get; set; }

    public int PlantillaId { get; set; }

    public string TipoPregunta { get; set; } = null!;

    public string Etiqueta { get; set; } = null!;

    public bool? Obligatorio { get; set; }

    public int Orden { get; set; }

    public virtual ICollection<FormularioOpcion> FormularioOpcions { get; set; } = new List<FormularioOpcion>();

    public virtual ICollection<FormularioRespuestum> FormularioRespuesta { get; set; } = new List<FormularioRespuestum>();

    public virtual FormularioPlantilla Plantilla { get; set; } = null!;
}
