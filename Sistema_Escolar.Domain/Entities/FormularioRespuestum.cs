using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class FormularioRespuestum
{
    public int RespuestaId { get; set; }

    public int EnvioId { get; set; }

    public int PreguntaId { get; set; }

    public string? TextoRespuesta { get; set; }

    public int? OpcionId { get; set; }

    public virtual FormularioEnvio Envio { get; set; } = null!;

    public virtual FormularioOpcion? Opcion { get; set; }

    public virtual FormularioPreguntum Pregunta { get; set; } = null!;
}
