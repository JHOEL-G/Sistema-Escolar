using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class RutaAprendizaje
{
    public int RutaId { get; set; }

    public string? NombreRuta { get; set; }

    public string? Descripcion { get; set; }

    public string? ImagenPortada { get; set; }

    public int? PrivacidadId { get; set; }

    public int? AsignarTemporal { get; set; }

    public int? ConfiguracionId { get; set; }

    public bool? CondicionAvanceCurso { get; set; }

    public bool? CondicionAvanceSeccion { get; set; }

    public string? CriterioAprobacion { get; set; }

    public int? CertificadoId { get; set; }

    public string? Gamificacion { get; set; }

    public string? MensajeBienvenida { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual TemporalRutum? AsignarTemporalNavigation { get; set; }

    public virtual Certificado? Certificado { get; set; }

    public virtual ICollection<CertificadorRutum> CertificadorRuta { get; set; } = new List<CertificadorRutum>();

    public virtual ConfiguracionParticipante? Configuracion { get; set; }

    public virtual ICollection<InscripcionRutum> InscripcionRuta { get; set; } = new List<InscripcionRutum>();

    public virtual Privacidad? Privacidad { get; set; }

    public virtual ICollection<RutaCurso> RutaCursos { get; set; } = new List<RutaCurso>();

    public virtual ICollection<SeccionRutum> SeccionRuta { get; set; } = new List<SeccionRutum>();
}
