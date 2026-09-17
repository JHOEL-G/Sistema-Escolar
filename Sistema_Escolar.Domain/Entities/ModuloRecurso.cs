using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class ModuloRecurso
{
    public int ModuloRecursoId { get; set; }

    public int? ModuloId { get; set; }

    public int? RecursoId { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public int? OrdenRecurso { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Modulo? Modulo { get; set; }

    public virtual ICollection<PreguntaVideoInteractivo> PreguntaVideoInteractivos { get; set; } = new List<PreguntaVideoInteractivo>();

    public virtual TipoRecurso? Recurso { get; set; }

    public virtual ICollection<RecursoEmbebido> RecursoEmbebidos { get; set; } = new List<RecursoEmbebido>();

    public virtual ICollection<RecursoEncuestum> RecursoEncuesta { get; set; } = new List<RecursoEncuestum>();

    public virtual ICollection<RecursoEvaluacionPresencial> RecursoEvaluacionPresencials { get; set; } = new List<RecursoEvaluacionPresencial>();

    public virtual ICollection<RecursoEvaluacion> RecursoEvaluacions { get; set; } = new List<RecursoEvaluacion>();

    public virtual ICollection<RecursoForo> RecursoForos { get; set; } = new List<RecursoForo>();

    public virtual ICollection<RecursoLectura> RecursoLecturas { get; set; } = new List<RecursoLectura>();

    public virtual ICollection<RecursoScorm> RecursoScorms { get; set; } = new List<RecursoScorm>();

    public virtual ICollection<RecursoSesionPresencial> RecursoSesionPresencials { get; set; } = new List<RecursoSesionPresencial>();

    public virtual ICollection<RecursoTarea> RecursoTareas { get; set; } = new List<RecursoTarea>();

    public virtual ICollection<RecursoVideo> RecursoVideos { get; set; } = new List<RecursoVideo>();

    public virtual ICollection<RecursoZoom> RecursoZooms { get; set; } = new List<RecursoZoom>();

    public virtual ICollection<UsuarioRecursoProgreso> UsuarioRecursoProgresos { get; set; } = new List<UsuarioRecursoProgreso>();
}
