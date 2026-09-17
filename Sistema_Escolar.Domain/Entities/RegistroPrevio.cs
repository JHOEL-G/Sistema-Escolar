using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RegistroPrevio
{
    public int PrevioId { get; set; }

    public string? NombreCurso { get; set; }

    public string? Descripcion { get; set; }

    public string? ImagenPath { get; set; }

    public string? DuracionCurso { get; set; }

    public string? MensajeBienvenida { get; set; }

    public int? InstructorId { get; set; }

    public bool? Recordatorio { get; set; }

    public virtual Usuario? Instructor { get; set; }
}
