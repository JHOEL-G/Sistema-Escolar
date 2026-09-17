using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ForoCalificacion
{
    public int ForoCalificacionId { get; set; }

    public int ForoId { get; set; }

    public int UsuarioId { get; set; }

    public int CursoId { get; set; }

    public decimal? Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public int? CalificadoPorId { get; set; }

    public virtual RecursoForo Foro { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
