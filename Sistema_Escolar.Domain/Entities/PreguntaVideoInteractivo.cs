using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class PreguntaVideoInteractivo
{
    public int PreguntaVideoId { get; set; }

    public string VideoPath { get; set; } = null!;

    public string TextoPregunta { get; set; } = null!;

    public int SegundoMarca { get; set; }

    public int TipoPreguntaId { get; set; }

    public decimal? PuntosValor { get; set; }

    public bool? Activo { get; set; }

    public int? ModuloRecursoId { get; set; }

    public virtual ModuloRecurso? ModuloRecurso { get; set; }

    public virtual ICollection<OpcionPreguntaVideo> OpcionPreguntaVideos { get; set; } = new List<OpcionPreguntaVideo>();
}
