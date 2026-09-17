using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EncuestaOpcion
{
    public int EncuestaOpcionId { get; set; }

    public int EncuestaPreguntaId { get; set; }

    public string? TituloOpcion { get; set; }

    public string? TextoOpcion { get; set; }

    public int? OrdenOpcion { get; set; }

    public virtual EncuestaPreguntum EncuestaPregunta { get; set; } = null!;

    public virtual ICollection<EncuestaRespuestum> EncuestaRespuesta { get; set; } = new List<EncuestaRespuestum>();
}
