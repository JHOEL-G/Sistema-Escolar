using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class TareaEntrega
{
    public int EntregaId { get; set; }

    public int TareaId { get; set; }

    public int UsuarioId { get; set; }

    public string? ArchivoPath { get; set; }

    public string? Comentario { get; set; }

    public decimal? Calificacion { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public int? CalificadoPorId { get; set; }

    public virtual Usuario? CalificadoPor { get; set; }

    public virtual RecursoTarea Tarea { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
