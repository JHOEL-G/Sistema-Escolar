using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EncuestaRespuestum
{
    public int EncuestaRespuestaId { get; set; }

    public int EncuestaPreguntaId { get; set; }

    public int UsuarioId { get; set; }

    public string? RespuestaTexto { get; set; }

    public int? OpcionSeleccionadaId { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public virtual EncuestaPreguntum EncuestaPregunta { get; set; } = null!;

    public virtual EncuestaOpcion? OpcionSeleccionada { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
