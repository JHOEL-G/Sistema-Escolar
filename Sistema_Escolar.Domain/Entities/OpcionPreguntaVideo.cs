using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class OpcionPreguntaVideo
{
    public int OpcionVideoId { get; set; }

    public int PreguntaVideoId { get; set; }

    public string TextoOpcion { get; set; } = null!;

    public bool EsCorrecta { get; set; }

    public virtual PreguntaVideoInteractivo PreguntaVideo { get; set; } = null!;
}
