using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class CursoParticipante
{
    public int CursoParticipanteId { get; set; }

    public int? GestionCursoId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public string? Estado { get; set; }

    public virtual GestionCurso? GestionCurso { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
