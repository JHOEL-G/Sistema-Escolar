using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EvaluacionCalificacion
{
    public int CalificacionId { get; set; }

    public int EvaluacionId { get; set; }

    public int UsuarioId { get; set; }

    public int CursoId { get; set; }

    public decimal Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public int? CalificadoPorId { get; set; }
}
