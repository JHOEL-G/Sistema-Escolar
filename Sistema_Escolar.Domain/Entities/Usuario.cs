using System;
using System.Collections.Generic;

namespace Sistema_Escolar_Confia.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public Guid KeycloakId { get; set; }

    public string? Nombre { get; set; }

    public string? ApeLlido { get; set; }

    public string Correo { get; set; } = null!;

    public string? ContraseñaHash { get; set; }

    public int? PuestoId { get; set; }

    public int? OrganizacionalesId { get; set; }

    public int? JefeId { get; set; }

    public string? CorreoAlternativo { get; set; }

    public string? Curp { get; set; }

    public DateOnly? FechaActivacion { get; set; }

    public DateOnly? FechaDesactivacion { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? IdEmpleado { get; set; }

    public string? RazonSocial { get; set; }

    public int? RolId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public int? NivelPermisoId { get; set; }

    public string? ImagenPortada { get; set; }

    public virtual ICollection<CertificadorRutum> CertificadorRuta { get; set; } = new List<CertificadorRutum>();

    public virtual ICollection<CursoEvaluadore> CursoEvaluadores { get; set; } = new List<CursoEvaluadore>();

    public virtual ICollection<CursoInstructor> CursoInstructors { get; set; } = new List<CursoInstructor>();

    public virtual ICollection<CursoParticipante> CursoParticipantes { get; set; } = new List<CursoParticipante>();

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual ICollection<EncuestaRespuestum> EncuestaRespuesta { get; set; } = new List<EncuestaRespuestum>();

    public virtual ICollection<EvaluacionPresencialCalificacion> EvaluacionPresencialCalificacionCalificadoPors { get; set; } = new List<EvaluacionPresencialCalificacion>();

    public virtual ICollection<EvaluacionPresencialCalificacion> EvaluacionPresencialCalificacionUsuarios { get; set; } = new List<EvaluacionPresencialCalificacion>();

    public virtual ICollection<EvaluacionRespuestum> EvaluacionRespuesta { get; set; } = new List<EvaluacionRespuestum>();

    public virtual ICollection<FormularioEnvio> FormularioEnvios { get; set; } = new List<FormularioEnvio>();

    public virtual ICollection<ForoCalificacion> ForoCalificacions { get; set; } = new List<ForoCalificacion>();

    public virtual ICollection<ForoPublicacion> ForoPublicacions { get; set; } = new List<ForoPublicacion>();

    public virtual ICollection<Fotousuario> Fotousuarios { get; set; } = new List<Fotousuario>();

    public virtual ICollection<GestionCurso> GestionCursos { get; set; } = new List<GestionCurso>();

    public virtual ICollection<InscripcionRutum> InscripcionRuta { get; set; } = new List<InscripcionRutum>();

    public virtual Jefedirecto? Jefe { get; set; }

    public virtual Permiso? NivelPermiso { get; set; }

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual UnidadesOrganizacionale? Organizacionales { get; set; }

    public virtual Puesto? Puesto { get; set; }

    public virtual ICollection<RegistroPrevio> RegistroPrevios { get; set; } = new List<RegistroPrevio>();

    public virtual CatRole? Rol { get; set; }

    public virtual ICollection<TareaEntrega> TareaEntregaCalificadoPors { get; set; } = new List<TareaEntrega>();

    public virtual ICollection<TareaEntrega> TareaEntregaUsuarios { get; set; } = new List<TareaEntrega>();

    public virtual ICollection<UsuarioCurso> UsuarioCursos { get; set; } = new List<UsuarioCurso>();

    public virtual ICollection<UsuarioRecursoProgreso> UsuarioRecursoProgresos { get; set; } = new List<UsuarioRecursoProgreso>();

    public virtual ICollection<UsuarioValor> UsuarioValors { get; set; } = new List<UsuarioValor>();
}
