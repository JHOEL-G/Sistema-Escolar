using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class EncuestaBanca
{
    public int EncuestaBancaId { get; set; }

    public int EncuestaId { get; set; }

    public int BancaId { get; set; }

    public int Cantidad { get; set; }

    public virtual RecursoEncuestum Encuesta { get; set; } = null!;
}
