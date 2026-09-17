using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class FormularioOpcion
{
    public int OpcionId { get; set; }

    public int PreguntaId { get; set; }

    public string TextoOpcion { get; set; } = null!;

    public int Orden { get; set; }

    public virtual ICollection<FormularioRespuestum> FormularioRespuesta { get; set; } = new List<FormularioRespuestum>();

    public virtual FormularioPreguntum Pregunta { get; set; } = null!;
}
