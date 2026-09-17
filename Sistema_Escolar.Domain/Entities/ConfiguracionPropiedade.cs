using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ConfiguracionPropiedade
{
    public int PropiedadId { get; set; }

    public string? NombrePropiedad { get; set; }

    public string? Etiqueta { get; set; }

    public int? TipoCampo { get; set; }

    public string? ValorDefecto { get; set; }

    public bool? EsRequerido { get; set; }

    public bool? UsarComoFiltro { get; set; }

    public bool? UsarEnReporte { get; set; }

    public virtual ICollection<ConfiguracionParticipante> ConfiguracionParticipantes { get; set; } = new List<ConfiguracionParticipante>();

    public virtual TipoCampo? TipoCampoNavigation { get; set; }

    public virtual ICollection<UsuarioValor> UsuarioValors { get; set; } = new List<UsuarioValor>();
}
