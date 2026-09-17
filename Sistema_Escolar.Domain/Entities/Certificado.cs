using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Certificado
{
    public int CertificadoId { get; set; }

    public string? NombreCertificado { get; set; }

    public virtual ICollection<RutaAprendizaje> RutaAprendizajes { get; set; } = new List<RutaAprendizaje>();
}
