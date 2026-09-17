using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class BancaPregunta
{
    public int BancaId { get; set; }

    public string? NombreBanca { get; set; }

    public string? Descripcion { get; set; }

    public string? ArchivoPlantilla { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<EvaluacionBanca> EvaluacionBancas { get; set; } = new List<EvaluacionBanca>();

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
