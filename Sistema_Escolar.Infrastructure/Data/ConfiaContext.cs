using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar_Confia.Models;

namespace Sistema_Escolar.Infrastructure.Data;

public partial class ConfiaContext : DbContext
{
    public ConfiaContext(DbContextOptions<ConfiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BancaPregunta> BancaPreguntas { get; set; }

    public virtual DbSet<CatRole> CatRoles { get; set; }

    public virtual DbSet<Certificado> Certificados { get; set; }

    public virtual DbSet<CertificadorRutum> CertificadorRuta { get; set; }

    public virtual DbSet<ConfiguracionParticipante> ConfiguracionParticipantes { get; set; }

    public virtual DbSet<ConfiguracionParticipantesDetalle> ConfiguracionParticipantesDetalles { get; set; }

    public virtual DbSet<ConfiguracionPropiedade> ConfiguracionPropiedades { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<CursoCriteriosInscripcion> CursoCriteriosInscripcions { get; set; }

    public virtual DbSet<CursoEvaluadore> CursoEvaluadores { get; set; }

    public virtual DbSet<CursoInstructor> CursoInstructors { get; set; }

    public virtual DbSet<CursoParticipante> CursoParticipantes { get; set; }

    public virtual DbSet<CursoReacreditacion> CursoReacreditacions { get; set; }

    public virtual DbSet<CursoTema> CursoTemas { get; set; }

    public virtual DbSet<Dificultad> Dificultads { get; set; }

    public virtual DbSet<DocumentoFormato> DocumentoFormatos { get; set; }

    public virtual DbSet<DocumentoRole> DocumentoRoles { get; set; }

    public virtual DbSet<EncuestaBanca> EncuestaBancas { get; set; }

    public virtual DbSet<EncuestaOpcion> EncuestaOpcions { get; set; }

    public virtual DbSet<EncuestaPreguntum> EncuestaPregunta { get; set; }

    public virtual DbSet<EncuestaRespuestum> EncuestaRespuesta { get; set; }

    public virtual DbSet<EvaluacionBanca> EvaluacionBancas { get; set; }

    public virtual DbSet<EvaluacionCalificacion> EvaluacionCalificacions { get; set; }

    public virtual DbSet<EvaluacionPreguntaOpcion> EvaluacionPreguntaOpcions { get; set; }

    public virtual DbSet<EvaluacionPreguntum> EvaluacionPregunta { get; set; }

    public virtual DbSet<EvaluacionPresencialCalificacion> EvaluacionPresencialCalificacions { get; set; }

    public virtual DbSet<EvaluacionPresencialCriterio> EvaluacionPresencialCriterios { get; set; }

    public virtual DbSet<EvaluacionPresencialRubrica> EvaluacionPresencialRubricas { get; set; }

    public virtual DbSet<EvaluacionRespuestum> EvaluacionRespuesta { get; set; }

    public virtual DbSet<ExploradorArchivo> ExploradorArchivos { get; set; }

    public virtual DbSet<Formato> Formatos { get; set; }

    public virtual DbSet<FormularioEnvio> FormularioEnvios { get; set; }

    public virtual DbSet<FormularioOpcion> FormularioOpcions { get; set; }

    public virtual DbSet<FormularioPlantilla> FormularioPlantillas { get; set; }

    public virtual DbSet<FormularioPreguntum> FormularioPregunta { get; set; }

    public virtual DbSet<FormularioRespuestum> FormularioRespuesta { get; set; }

    public virtual DbSet<ForoCalificacion> ForoCalificacions { get; set; }

    public virtual DbSet<ForoPublicacion> ForoPublicacions { get; set; }

    public virtual DbSet<Fotousuario> Fotousuarios { get; set; }

    public virtual DbSet<GestionCurso> GestionCursos { get; set; }

    public virtual DbSet<GestionCursoVisibilidad> GestionCursoVisibilidads { get; set; }

    public virtual DbSet<GestionDocumeto> GestionDocumetos { get; set; }

    public virtual DbSet<InscripcionRutum> InscripcionRuta { get; set; }

    public virtual DbSet<Jefedirecto> Jefedirectos { get; set; }

    public virtual DbSet<LecturaArchivoAdjunto> LecturaArchivoAdjuntos { get; set; }

    public virtual DbSet<Lenguaje> Lenguajes { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<ModuloRecurso> ModuloRecursos { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<OpcionPreguntaVideo> OpcionPreguntaVideos { get; set; }

    public virtual DbSet<OpcionRespuestum> OpcionRespuesta { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PreguntaVideoInteractivo> PreguntaVideoInteractivos { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Privacidad> Privacidads { get; set; }

    public virtual DbSet<PrivacidadUo> PrivacidadUos { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<RecursoEmbebido> RecursoEmbebidos { get; set; }

    public virtual DbSet<RecursoEncuestum> RecursoEncuesta { get; set; }

    public virtual DbSet<RecursoEvaluacion> RecursoEvaluacions { get; set; }

    public virtual DbSet<RecursoEvaluacionPresencial> RecursoEvaluacionPresencials { get; set; }

    public virtual DbSet<RecursoForo> RecursoForos { get; set; }

    public virtual DbSet<RecursoLectura> RecursoLecturas { get; set; }

    public virtual DbSet<RecursoScorm> RecursoScorms { get; set; }

    public virtual DbSet<RecursoSesionPresencial> RecursoSesionPresencials { get; set; }

    public virtual DbSet<RecursoTarea> RecursoTareas { get; set; }

    public virtual DbSet<RecursoVideo> RecursoVideos { get; set; }

    public virtual DbSet<RecursoZoom> RecursoZooms { get; set; }

    public virtual DbSet<RegistroPrevio> RegistroPrevios { get; set; }

    public virtual DbSet<Retroalimentacion> Retroalimentacions { get; set; }

    public virtual DbSet<Rubrica> Rubricas { get; set; }

    public virtual DbSet<RubricaCalificacion> RubricaCalificacions { get; set; }

    public virtual DbSet<RubricaCriterio> RubricaCriterios { get; set; }

    public virtual DbSet<RutaAprendizaje> RutaAprendizajes { get; set; }

    public virtual DbSet<RutaCurso> RutaCursos { get; set; }

    public virtual DbSet<SeccionCurso> SeccionCursos { get; set; }

    public virtual DbSet<SeccionRutum> SeccionRuta { get; set; }

    public virtual DbSet<SistemaConfiguracion> SistemaConfiguracions { get; set; }

    public virtual DbSet<TareaEntrega> TareaEntregas { get; set; }

    public virtual DbSet<Tema> Temas { get; set; }

    public virtual DbSet<TemporalRutum> TemporalRuta { get; set; }

    public virtual DbSet<TipoCalificacion> TipoCalificacions { get; set; }

    public virtual DbSet<TipoCampo> TipoCampos { get; set; }

    public virtual DbSet<TipoLectura> TipoLecturas { get; set; }

    public virtual DbSet<TipoPreguntum> TipoPregunta { get; set; }

    public virtual DbSet<TipoRecurso> TipoRecursos { get; set; }

    public virtual DbSet<UnidadesOrganizacionale> UnidadesOrganizacionales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioCurso> UsuarioCursos { get; set; }

    public virtual DbSet<UsuarioRecursoProgreso> UsuarioRecursoProgresos { get; set; }

    public virtual DbSet<UsuarioValor> UsuarioValors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BancaPregunta>(entity =>
        {
            entity.HasKey(e => e.BancaId).HasName("PK__BANCA_PR__48FD569B4196D518");

            entity.ToTable("BANCA_PREGUNTAS");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreBanca).HasMaxLength(200);
        });

        modelBuilder.Entity<CatRole>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__CAT_ROLE__F92302F1C1880DBF");

            entity.ToTable("CAT_ROLES");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.NombreRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Certificado>(entity =>
        {
            entity.HasKey(e => e.CertificadoId).HasName("PK__CERTIFIC__6B0167139B86528D");

            entity.ToTable("CERTIFICADO");

            entity.Property(e => e.NombreCertificado).HasMaxLength(100);
        });

        modelBuilder.Entity<CertificadorRutum>(entity =>
        {
            entity.HasKey(e => e.CertificadorRutaId).HasName("PK__CERTIFIC__E221625F873A13DA");

            entity.ToTable("CERTIFICADOR_RUTA");

            entity.HasIndex(e => new { e.RutaId, e.UsuarioId }, "UQ_CertRuta").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Ruta).WithMany(p => p.CertificadorRuta)
                .HasForeignKey(d => d.RutaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertRuta_Ruta");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CertificadorRuta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertRuta_Usuario");
        });

        modelBuilder.Entity<ConfiguracionParticipante>(entity =>
        {
            entity.HasKey(e => e.ConfiguracionId).HasName("PK__CONFIGUR__9B95E036E287B0B6");

            entity.ToTable("CONFIGURACION_PARTICIPANTES");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.PermiteDesinscripcion).HasDefaultValue(false);

            entity.HasOne(d => d.Propiedades).WithMany(p => p.ConfiguracionParticipantes)
                .HasForeignKey(d => d.PropiedadesId)
                .HasConstraintName("FK__CONFIGURA__Propi__4B422AD5");

            entity.HasOne(d => d.Roles).WithMany(p => p.ConfiguracionParticipantes)
                .HasForeignKey(d => d.RolesId)
                .HasConstraintName("FK__CONFIGURA__Roles__4C364F0E");

            entity.HasOne(d => d.Uo).WithMany(p => p.ConfiguracionParticipantes)
                .HasForeignKey(d => d.UoId)
                .HasConstraintName("FK__CONFIGURAC__UoId__4D2A7347");
        });

        modelBuilder.Entity<ConfiguracionParticipantesDetalle>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__CONFIGUR__6E19D6DA922F701B");

            entity.ToTable("CONFIGURACION_PARTICIPANTES_DETALLE");

            entity.Property(e => e.TipoEntidad).HasMaxLength(50);

            entity.HasOne(d => d.Config).WithMany(p => p.ConfiguracionParticipantesDetalles)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CONFIGURA__Confi__08D548FA");
        });

        modelBuilder.Entity<ConfiguracionPropiedade>(entity =>
        {
            entity.HasKey(e => e.PropiedadId).HasName("PK__Configur__D4B8C06D6B96AD6C");

            entity.ToTable("Configuracion_Propiedades");

            entity.Property(e => e.EsRequerido).HasDefaultValue(false);
            entity.Property(e => e.Etiqueta).HasMaxLength(100);
            entity.Property(e => e.NombrePropiedad).HasMaxLength(100);
            entity.Property(e => e.UsarComoFiltro).HasDefaultValue(false);
            entity.Property(e => e.UsarEnReporte).HasDefaultValue(false);

            entity.HasOne(d => d.TipoCampoNavigation).WithMany(p => p.ConfiguracionPropiedades)
                .HasForeignKey(d => d.TipoCampo)
                .HasConstraintName("FK__Configura__TipoC__4E1E9780");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__CURSO__7E0235D76BE6A454");

            entity.ToTable("CURSO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Avance).HasMaxLength(100);
            entity.Property(e => e.DuracionCurso).HasMaxLength(100);
            entity.Property(e => e.EstaPublicado).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HhabilitarFechaCurso).HasDefaultValue(false);
            entity.Property(e => e.NombreCurso).HasMaxLength(200);
            entity.Property(e => e.PorQueInscribirmeCurso).HasMaxLength(500);
            entity.Property(e => e.Reacreditación).HasDefaultValue(false);

            entity.HasOne(d => d.Dificultad).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.DificultadId)
                .HasConstraintName("FK__CURSO__Dificulta__4F12BBB9");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK__CURSO__Instructo__5006DFF2");

            entity.HasOne(d => d.Lenguaje).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.LenguajeId)
                .HasConstraintName("FK__CURSO__LenguajeI__50FB042B");

            entity.HasOne(d => d.Retroalimentacion).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.RetroalimentacionId)
                .HasConstraintName("FK__CURSO__Retroalim__51EF2864");
        });

        modelBuilder.Entity<CursoCriteriosInscripcion>(entity =>
        {
            entity.HasKey(e => e.CriterioId).HasName("PK__CURSO_CR__11080FC6AC714348");

            entity.ToTable("CURSO_CRITERIOS_INSCRIPCION");

            entity.Property(e => e.OperadorLogico)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValue("Y");
            entity.Property(e => e.TipoCriterio)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ValorPropiedad).HasMaxLength(255);

            entity.HasOne(d => d.GestionCurso).WithMany(p => p.CursoCriteriosInscripcions)
                .HasForeignKey(d => d.GestionCursoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CURSO_CRI__Gesti__52E34C9D");
        });

        modelBuilder.Entity<CursoEvaluadore>(entity =>
        {
            entity.HasKey(e => e.CursoEvaluadorId).HasName("PK__CURSO_EV__C014C4352A5BFFF2");

            entity.ToTable("CURSO_EVALUADORES");

            entity.HasIndex(e => new { e.GestionCursoId, e.UsuarioId }, "UQ__CURSO_EV__89FDF82D9D3A5A76").IsUnique();

            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.GestionCurso).WithMany(p => p.CursoEvaluadores)
                .HasForeignKey(d => d.GestionCursoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CURSO_EVA__Gesti__53D770D6");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CursoEvaluadores)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__CURSO_EVA__Usuar__54CB950F");
        });

        modelBuilder.Entity<CursoInstructor>(entity =>
        {
            entity.HasKey(e => e.CursoInstructorId).HasName("PK__CURSO_IN__221338C63871EDFA");

            entity.ToTable("CURSO_INSTRUCTOR");

            entity.HasIndex(e => new { e.CursoId, e.InstructorId }, "UQ_Curso_Instructor").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.EsInstructorPrincipal).HasDefaultValue(false);
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Curso).WithMany(p => p.CursoInstructors)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CURSO_INS__Curso__55BFB948");

            entity.HasOne(d => d.Instructor).WithMany(p => p.CursoInstructors)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CURSO_INS__Instr__56B3DD81");
        });

        modelBuilder.Entity<CursoParticipante>(entity =>
        {
            entity.HasKey(e => e.CursoParticipanteId).HasName("PK__CURSO_PA__611AAE5BAE609B01");

            entity.ToTable("CURSO_PARTICIPANTES");

            entity.HasIndex(e => new { e.GestionCursoId, e.UsuarioId }, "UQ__CURSO_PA__89FDF82DE783DFA9").IsUnique();

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activo");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.GestionCurso).WithMany(p => p.CursoParticipantes)
                .HasForeignKey(d => d.GestionCursoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CURSO_PAR__Gesti__57A801BA");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CursoParticipantes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__CURSO_PAR__Usuar__589C25F3");
        });

        modelBuilder.Entity<CursoReacreditacion>(entity =>
        {
            entity.HasKey(e => e.ReacreditacionId).HasName("PK__CURSO_RE__2496FD7AE700519D");

            entity.ToTable("CURSO_REACREDITACION");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Curso).WithMany(p => p.CursoReacreditacionCursos)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CURSO_REA__Curso__3943762B");

            entity.HasOne(d => d.CursoReacreditacionNavigation).WithMany(p => p.CursoReacreditacionCursoReacreditacionNavigations)
                .HasForeignKey(d => d.CursoReacreditacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CURSO_REA__Curso__3A379A64");
        });

        modelBuilder.Entity<CursoTema>(entity =>
        {
            entity.HasKey(e => e.CursoTemaId).HasName("PK__CURSO_TE__7707985B7A9742DF");

            entity.ToTable("CURSO_TEMAS");

            entity.Property(e => e.Orden).HasDefaultValue(0);

            entity.HasOne(d => d.GestionCurso).WithMany(p => p.CursoTemas)
                .HasForeignKey(d => d.GestionCursoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CURSO_TEM__Gesti__59904A2C");

            entity.HasOne(d => d.Tema).WithMany(p => p.CursoTemas)
                .HasForeignKey(d => d.TemaId)
                .HasConstraintName("FK__CURSO_TEM__TemaI__5A846E65");
        });

        modelBuilder.Entity<Dificultad>(entity =>
        {
            entity.HasKey(e => e.DificultadId).HasName("PK__DIFICULT__64A2CB0B09DD9AA2");

            entity.ToTable("DIFICULTAD");
        });

        modelBuilder.Entity<DocumentoFormato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC075B2416FD");

            entity.ToTable("Documento_Formatos");

            entity.HasIndex(e => new { e.GestionId, e.FormatoId }, "UQ__Document__351AC3FE9DF513DA").IsUnique();

            entity.HasOne(d => d.Formato).WithMany(p => p.DocumentoFormatos)
                .HasForeignKey(d => d.FormatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Forma__5B78929E");

            entity.HasOne(d => d.Gestion).WithMany(p => p.DocumentoFormatos)
                .HasForeignKey(d => d.GestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Gesti__5C6CB6D7");
        });

        modelBuilder.Entity<DocumentoRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07A10A4A18");

            entity.ToTable("Documento_Roles");

            entity.HasIndex(e => new { e.GestionId, e.RolId }, "UQ__Document__77AA6CCCEA14290C").IsUnique();

            entity.HasOne(d => d.Gestion).WithMany(p => p.DocumentoRoles)
                .HasForeignKey(d => d.GestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Gesti__5D60DB10");

            entity.HasOne(d => d.Rol).WithMany(p => p.DocumentoRoles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__RolId__5E54FF49");
        });

        modelBuilder.Entity<EncuestaBanca>(entity =>
        {
            entity.HasKey(e => e.EncuestaBancaId).HasName("PK__ENCUESTA__68DC117EC8476391");

            entity.ToTable("ENCUESTA_BANCA");

            entity.HasOne(d => d.Encuesta).WithMany(p => p.EncuestaBancas)
                .HasForeignKey(d => d.EncuestaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ENCUESTA___Encue__381A47C8");
        });

        modelBuilder.Entity<EncuestaOpcion>(entity =>
        {
            entity.HasKey(e => e.EncuestaOpcionId).HasName("PK__ENCUESTA__A72AF6C0E0FB55AC");

            entity.ToTable("ENCUESTA_OPCION");

            entity.Property(e => e.TituloOpcion).HasMaxLength(200);

            entity.HasOne(d => d.EncuestaPregunta).WithMany(p => p.EncuestaOpcions)
                .HasForeignKey(d => d.EncuestaPreguntaId)
                .HasConstraintName("FK_EncuestaOpcion_Pregunta");
        });

        modelBuilder.Entity<EncuestaPreguntum>(entity =>
        {
            entity.HasKey(e => e.EncuestaPreguntaId).HasName("PK__ENCUESTA__3DD135A91568B6A7");

            entity.ToTable("ENCUESTA_PREGUNTA");

            entity.HasIndex(e => e.EncuestaId, "IX_EncuestaPregunta_Encuesta");

            entity.Property(e => e.TipoPregunta).HasMaxLength(50);

            entity.HasOne(d => d.Encuesta).WithMany(p => p.EncuestaPregunta)
                .HasForeignKey(d => d.EncuestaId)
                .HasConstraintName("FK_EncuestaPregunta_Encuesta");
        });

        modelBuilder.Entity<EncuestaRespuestum>(entity =>
        {
            entity.HasKey(e => e.EncuestaRespuestaId).HasName("PK__ENCUESTA__4DB1A3923A8D74EF");

            entity.ToTable("ENCUESTA_RESPUESTA");

            entity.Property(e => e.FechaRespuesta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.EncuestaPregunta).WithMany(p => p.EncuestaRespuesta)
                .HasForeignKey(d => d.EncuestaPreguntaId)
                .HasConstraintName("FK_EncuestaRespuesta_Pregunta");

            entity.HasOne(d => d.OpcionSeleccionada).WithMany(p => p.EncuestaRespuesta)
                .HasForeignKey(d => d.OpcionSeleccionadaId)
                .HasConstraintName("FK_EncuestaRespuesta_Opcion");

            entity.HasOne(d => d.Usuario).WithMany(p => p.EncuestaRespuesta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncuestaRespuesta_Usuario");
        });

        modelBuilder.Entity<EvaluacionBanca>(entity =>
        {
            entity.HasKey(e => e.EvaluacionBancaId).HasName("PK__EVALUACI__CF38A7806C36119B");

            entity.ToTable("EVALUACION_BANCA");

            entity.HasIndex(e => e.EvaluacionId, "IX_EvaluacionBanca_Evaluacion");

            entity.HasOne(d => d.Banca).WithMany(p => p.EvaluacionBancas)
                .HasForeignKey(d => d.BancaId)
                .HasConstraintName("FK__EVALUACIO__Banca__640DD89F");

            entity.HasOne(d => d.Evaluacion).WithMany(p => p.EvaluacionBancas)
                .HasForeignKey(d => d.EvaluacionId)
                .HasConstraintName("FK__EVALUACIO__Evalu__6501FCD8");
        });

        modelBuilder.Entity<EvaluacionCalificacion>(entity =>
        {
            entity.HasKey(e => e.CalificacionId).HasName("PK__EVALUACI__4CF54ADE6D6F60B9");

            entity.ToTable("EVALUACION_CALIFICACION");

            entity.Property(e => e.Calificacion).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaCalificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EvaluacionPreguntaOpcion>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("PK__EVALUACI__77CD0863152EEE35");

            entity.ToTable("EVALUACION_PREGUNTA_OPCION");

            entity.Property(e => e.EsCorrecta).HasDefaultValue(false);
            entity.Property(e => e.ExplicacionOrelacion).HasColumnName("ExplicacionORelacion");
            entity.Property(e => e.Orden).HasDefaultValue(0);

            entity.HasOne(d => d.EvaluacionPregunta).WithMany(p => p.EvaluacionPreguntaOpcions)
                .HasForeignKey(d => d.EvaluacionPreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Evalu__21A0F6C4");
        });

        modelBuilder.Entity<EvaluacionPreguntum>(entity =>
        {
            entity.HasKey(e => e.EvaluacionPreguntaId).HasName("PK__EVALUACI__3F4D4785F33F0EEC");

            entity.ToTable("EVALUACION_PREGUNTA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasDefaultValue(0);
            entity.Property(e => e.PuntosValor)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Evaluacion).WithMany(p => p.EvaluacionPregunta)
                .HasForeignKey(d => d.EvaluacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Evalu__1CDC41A7");
        });

        modelBuilder.Entity<EvaluacionPresencialCalificacion>(entity =>
        {
            entity.HasKey(e => e.EvaluacionCalificacionId).HasName("PK__EVALUACI__97E54F9AAD2FBC3F");

            entity.ToTable("EVALUACION_PRESENCIAL_CALIFICACION");

            entity.Property(e => e.FechaCalificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CalificacionRubrica).WithMany(p => p.EvaluacionPresencialCalificacions)
                .HasForeignKey(d => d.CalificacionRubricaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvalPresCalif_CalificacionRubrica");

            entity.HasOne(d => d.CalificadoPor).WithMany(p => p.EvaluacionPresencialCalificacionCalificadoPors)
                .HasForeignKey(d => d.CalificadoPorId)
                .HasConstraintName("FK_EvalPresCalif_Calificador");

            entity.HasOne(d => d.Criterio).WithMany(p => p.EvaluacionPresencialCalificacions)
                .HasForeignKey(d => d.CriterioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvalPresCalif_Criterio");

            entity.HasOne(d => d.EvaluacionPresencial).WithMany(p => p.EvaluacionPresencialCalificacions)
                .HasForeignKey(d => d.EvaluacionPresencialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvalPresCalif_EvalPresencial");

            entity.HasOne(d => d.Usuario).WithMany(p => p.EvaluacionPresencialCalificacionUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvalPresCalif_Usuario");
        });

        modelBuilder.Entity<EvaluacionPresencialCriterio>(entity =>
        {
            entity.HasKey(e => e.CriterioId).HasName("PK__EVALUACI__11080FC6D2633BEC");

            entity.ToTable("EVALUACION_PRESENCIAL_CRITERIO");

            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.Rubrica).WithMany(p => p.EvaluacionPresencialCriterios)
                .HasForeignKey(d => d.RubricaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Rubri__4D1564AE");
        });

        modelBuilder.Entity<EvaluacionPresencialRubrica>(entity =>
        {
            entity.HasKey(e => e.RubricaId).HasName("PK__EVALUACI__D5D1E235C15F1279");

            entity.ToTable("EVALUACION_PRESENCIAL_RUBRICA");

            entity.Property(e => e.Nombre).HasMaxLength(200);

            entity.HasOne(d => d.EvaluacionPresencial).WithMany(p => p.EvaluacionPresencialRubricas)
                .HasForeignKey(d => d.EvaluacionPresencialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Evalu__4A38F803");
        });

        modelBuilder.Entity<EvaluacionRespuestum>(entity =>
        {
            entity.HasKey(e => e.RespuestaId).HasName("PK__EVALUACI__31F7FC11E97CB42A");

            entity.ToTable("EVALUACION_RESPUESTA");

            entity.Property(e => e.EsCorrecta).HasDefaultValue(false);
            entity.Property(e => e.FechaRespuesta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Intento).HasDefaultValue(1);
            entity.Property(e => e.PuntosObtenidos)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Evaluacion).WithMany(p => p.EvaluacionRespuesta)
                .HasForeignKey(d => d.EvaluacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Evalu__6ABAD62E");

            entity.HasOne(d => d.Opcion).WithMany(p => p.EvaluacionRespuesta)
                .HasForeignKey(d => d.OpcionId)
                .HasConstraintName("FK__EVALUACIO__Opcio__6BAEFA67");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.EvaluacionRespuesta)
                .HasForeignKey(d => d.PreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Pregu__6CA31EA0");

            entity.HasOne(d => d.Usuario).WithMany(p => p.EvaluacionRespuesta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUACIO__Usuar__6D9742D9");
        });

        modelBuilder.Entity<ExploradorArchivo>(entity =>
        {
            entity.HasKey(e => e.ExploradorId).HasName("PK__Explorad__CE6F8E6769F0E385");

            entity.ToTable("Explorador_Archivo");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCarpeta).HasMaxLength(500);

            entity.HasOne(d => d.Padre).WithMany(p => p.InversePadre)
                .HasForeignKey(d => d.PadreId)
                .HasConstraintName("FK__Explorado__Padre__6E8B6712");
        });

        modelBuilder.Entity<Formato>(entity =>
        {
            entity.HasKey(e => e.FormatoId).HasName("PK__Formato__D229F1D55EF05194");

            entity.ToTable("Formato");

            entity.Property(e => e.NombreFormato).HasMaxLength(100);
        });

        modelBuilder.Entity<FormularioEnvio>(entity =>
        {
            entity.HasKey(e => e.EnvioId).HasName("PK__FORMULAR__D024E23F0477AFE9");

            entity.ToTable("FORMULARIO_ENVIO");

            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Plantilla).WithMany(p => p.FormularioEnvios)
                .HasForeignKey(d => d.PlantillaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULARI__Plant__4B973090");

            entity.HasOne(d => d.Usuario).WithMany(p => p.FormularioEnvios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__FORMULARI__Usuar__4C8B54C9");
        });

        modelBuilder.Entity<FormularioOpcion>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("PK__FORMULAR__77CD08635AD94F8A");

            entity.ToTable("FORMULARIO_OPCION");

            entity.Property(e => e.TextoOpcion).HasMaxLength(300);

            entity.HasOne(d => d.Pregunta).WithMany(p => p.FormularioOpcions)
                .HasForeignKey(d => d.PreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULARI__Pregu__48BAC3E5");
        });

        modelBuilder.Entity<FormularioPlantilla>(entity =>
        {
            entity.HasKey(e => e.PlantillaId).HasName("PK__FORMULAR__C5DEB5EC8017B25D");

            entity.ToTable("FORMULARIO_PLANTILLA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");
            entity.Property(e => e.PublicId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Publicado).HasDefaultValue(false);
            entity.Property(e => e.Titulo).HasMaxLength(200);
        });

        modelBuilder.Entity<FormularioPreguntum>(entity =>
        {
            entity.HasKey(e => e.PreguntaId).HasName("PK__FORMULAR__EBB2A3799B8FD36D");

            entity.ToTable("FORMULARIO_PREGUNTA");

            entity.Property(e => e.Etiqueta).HasMaxLength(500);
            entity.Property(e => e.Obligatorio).HasDefaultValue(false);
            entity.Property(e => e.TipoPregunta).HasMaxLength(50);

            entity.HasOne(d => d.Plantilla).WithMany(p => p.FormularioPregunta)
                .HasForeignKey(d => d.PlantillaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULARI__Plant__44EA3301");
        });

        modelBuilder.Entity<FormularioRespuestum>(entity =>
        {
            entity.HasKey(e => e.RespuestaId).HasName("PK__FORMULAR__31F7FC111D25CADA");

            entity.ToTable("FORMULARIO_RESPUESTA");

            entity.HasOne(d => d.Envio).WithMany(p => p.FormularioRespuesta)
                .HasForeignKey(d => d.EnvioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULARI__Envio__505BE5AD");

            entity.HasOne(d => d.Opcion).WithMany(p => p.FormularioRespuesta)
                .HasForeignKey(d => d.OpcionId)
                .HasConstraintName("FK__FORMULARI__Opcio__52442E1F");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.FormularioRespuesta)
                .HasForeignKey(d => d.PreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULARI__Pregu__515009E6");
        });

        modelBuilder.Entity<ForoCalificacion>(entity =>
        {
            entity.HasKey(e => e.ForoCalificacionId).HasName("PK__FORO_CAL__5D480642AEC343C6");

            entity.ToTable("FORO_CALIFICACION");

            entity.HasIndex(e => new { e.ForoId, e.UsuarioId }, "UQ_Foro_Usuario").IsUnique();

            entity.Property(e => e.Calificacion)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaCalificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Foro).WithMany(p => p.ForoCalificacions)
                .HasForeignKey(d => d.ForoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORO_CALI__ForoI__6F7F8B4B");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ForoCalificacions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORO_CALI__Usuar__7073AF84");
        });

        modelBuilder.Entity<ForoPublicacion>(entity =>
        {
            entity.HasKey(e => e.PublicacionId).HasName("PK__FORO_PUB__10DF158ABEF58A82");

            entity.ToTable("FORO_PUBLICACION");

            entity.HasIndex(e => e.ForoId, "IX_ForoPublicacion_Foro");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ArchivoPath).HasMaxLength(500);
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Foro).WithMany(p => p.ForoPublicacions)
                .HasForeignKey(d => d.ForoId)
                .HasConstraintName("FK_ForoPublicacion_Foro");

            entity.HasOne(d => d.PublicacionPadre).WithMany(p => p.InversePublicacionPadre)
                .HasForeignKey(d => d.PublicacionPadreId)
                .HasConstraintName("FK_ForoPublicacion_Padre");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ForoPublicacions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ForoPublicacion_Usuario");
        });

        modelBuilder.Entity<Fotousuario>(entity =>
        {
            entity.HasKey(e => e.FotoId).HasName("PK__FOTOUSUA__4EA1C11996BC3E10");

            entity.ToTable("FOTOUSUARIO");

            entity.Property(e => e.FechaSubida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(500);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Fotousuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FOTOUSUAR__Usuar__74444068");
        });

        modelBuilder.Entity<GestionCurso>(entity =>
        {
            entity.HasKey(e => e.GestionCursoId).HasName("PK__GESTION___1B4E26579D213D07");

            entity.ToTable("GESTION_CURSOS");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gamificacion).HasDefaultValue(false);
            entity.Property(e => e.InscripcionAutomatica).HasDefaultValue(false);
            entity.Property(e => e.NombreCurso).HasMaxLength(255);
            entity.Property(e => e.PermitirDesinscripcion).HasDefaultValue(false);
            entity.Property(e => e.Privacidad)
                .HasMaxLength(50)
                .HasDefaultValue("Privado");

            entity.HasOne(d => d.CreadoPorNavigation).WithMany(p => p.GestionCursos)
                .HasForeignKey(d => d.CreadoPor)
                .HasConstraintName("FK__GESTION_C__Cread__753864A1");

            entity.HasOne(d => d.Curso).WithMany(p => p.GestionCursos)
                .HasForeignKey(d => d.CursoId)
                .HasConstraintName("FK__GESTION_C__Curso__762C88DA");
        });

        modelBuilder.Entity<GestionCursoVisibilidad>(entity =>
        {
            entity.HasKey(e => e.VisibilidadId).HasName("PK__GESTION___BC9673BAD5019FF4");

            entity.ToTable("GESTION_CURSO_VISIBILIDAD");

            entity.Property(e => e.TipoGrupo)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.GestionCurso).WithMany(p => p.GestionCursoVisibilidads)
                .HasForeignKey(d => d.GestionCursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GESTION_C__Gesti__39AD8A7F");
        });

        modelBuilder.Entity<GestionDocumeto>(entity =>
        {
            entity.HasKey(e => e.GestionId).HasName("PK__Gestion___78385CE2CCD2F441");

            entity.ToTable("Gestion_Documetos");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.TituloDocumento).HasMaxLength(300);

            entity.HasOne(d => d.Explorador).WithMany(p => p.GestionDocumetos)
                .HasForeignKey(d => d.ExploradorId)
                .HasConstraintName("FK__Gestion_D__Explo__7720AD13");

            entity.HasOne(d => d.FormatoPermitido).WithMany(p => p.GestionDocumetos)
                .HasForeignKey(d => d.FormatoPermitidoId)
                .HasConstraintName("FK__Gestion_D__Forma__7814D14C");

            entity.HasOne(d => d.RolesAcceso).WithMany(p => p.GestionDocumetos)
                .HasForeignKey(d => d.RolesAccesoId)
                .HasConstraintName("FK__Gestion_D__Roles__7908F585");
        });

        modelBuilder.Entity<InscripcionRutum>(entity =>
        {
            entity.HasKey(e => e.InscripcionRutaId).HasName("PK__INSCRIPC__E6320774659D4065");

            entity.ToTable("INSCRIPCION_RUTA");

            entity.HasIndex(e => new { e.RutaId, e.UsuarioId }, "UQ__INSCRIPC__E9D247F4BF83D927").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CalificacionFinal).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Completado).HasDefaultValue(false);
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Progreso)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Ruta).WithMany(p => p.InscripcionRuta)
                .HasForeignKey(d => d.RutaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__INSCRIPCI__RutaI__789EE131");

            entity.HasOne(d => d.Usuario).WithMany(p => p.InscripcionRuta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__INSCRIPCI__Usuar__7993056A");
        });

        modelBuilder.Entity<Jefedirecto>(entity =>
        {
            entity.HasKey(e => e.JefeId).HasName("PK__JEFEDIRE__74208DF48BE37246");

            entity.ToTable("JEFEDIRECTO");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<LecturaArchivoAdjunto>(entity =>
        {
            entity.HasKey(e => e.ArchivoAdjuntoId).HasName("PK__LECTURA___B8179CB44FECEB67");

            entity.ToTable("LECTURA_ARCHIVO_ADJUNTO");

            entity.HasIndex(e => e.LecturaId, "IX_LecturaArchivo_Lectura");

            entity.Property(e => e.FechaSubida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(200);
            entity.Property(e => e.TamañoMb)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TamañoMB");
            entity.Property(e => e.TipoArchivo).HasMaxLength(100);

            entity.HasOne(d => d.Lectura).WithMany(p => p.LecturaArchivoAdjuntos)
                .HasForeignKey(d => d.LecturaId)
                .HasConstraintName("FK_LecturaArchivo_Lectura");
        });

        modelBuilder.Entity<Lenguaje>(entity =>
        {
            entity.HasKey(e => e.LenguajeId).HasName("PK__LENGUAJE__541369DBD00D38C6");

            entity.ToTable("LENGUAJE");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.ModuloId).HasName("PK__MODULOS__26CEB8EF9E8A7A0A");

            entity.ToTable("MODULOS");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModuloTitulo).HasMaxLength(200);

            entity.HasOne(d => d.Curso).WithMany(p => p.Modulos)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MODULOS__CursoId__7CD98669");
        });

        modelBuilder.Entity<ModuloRecurso>(entity =>
        {
            entity.HasKey(e => e.ModuloRecursoId).HasName("PK__MODULO_R__E34616331F519C4A");

            entity.ToTable("MODULO_RECURSO");

            entity.HasIndex(e => e.ModuloId, "IX_ModuloRecurso_Modulo");

            entity.HasIndex(e => e.RecursoId, "IX_ModuloRecurso_Recurso");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.Modulo).WithMany(p => p.ModuloRecursos)
                .HasForeignKey(d => d.ModuloId)
                .HasConstraintName("FK__MODULO_RE__Modul__7AF13DF7");

            entity.HasOne(d => d.Recurso).WithMany(p => p.ModuloRecursos)
                .HasForeignKey(d => d.RecursoId)
                .HasConstraintName("FK__MODULO_RE__Recur__7BE56230");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120249F63836C");

            entity.ToTable("Notificacion");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Mensaje).HasMaxLength(500);
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__Usuar__79E80B25");
        });

        modelBuilder.Entity<OpcionPreguntaVideo>(entity =>
        {
            entity.HasKey(e => e.OpcionVideoId).HasName("PK__OPCION_P__A5A7247D0FC210EA");

            entity.ToTable("OPCION_PREGUNTA_VIDEO");

            entity.HasOne(d => d.PreguntaVideo).WithMany(p => p.OpcionPreguntaVideos)
                .HasForeignKey(d => d.PreguntaVideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OPCION_PR__Pregu__7DCDAAA2");
        });

        modelBuilder.Entity<OpcionRespuestum>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("PK__OPCION_R__77CD086396A4295C");

            entity.ToTable("OPCION_RESPUESTA");

            entity.Property(e => e.EsCorrecta).HasDefaultValue(false);
            entity.Property(e => e.ExplicacionOrelacion).HasColumnName("ExplicacionORelacion");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.OpcionRespuesta)
                .HasForeignKey(d => d.PreguntaId)
                .HasConstraintName("FK__OPCION_RE__Pregu__7EC1CEDB");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.PermisoId).HasName("PK__PERMISOS__96E0C723FA62F21B");

            entity.ToTable("PERMISOS");

            entity.Property(e => e.NombrePermiso).HasMaxLength(100);
        });

        modelBuilder.Entity<PreguntaVideoInteractivo>(entity =>
        {
            entity.HasKey(e => e.PreguntaVideoId).HasName("PK__PREGUNTA__BE5605F62F48D533");

            entity.ToTable("PREGUNTA_VIDEO_INTERACTIVO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.PuntosValor)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.PreguntaVideoInteractivos)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_PreguntaVideo_ModuloRecurso");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.PreguntaId).HasName("PK__PREGUNTA__EBB2A3795E1DE605");

            entity.ToTable("PREGUNTA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.PuntosValor)
                .HasDefaultValue(1.00m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Banca).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.BancaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PREGUNTA__BancaI__7FB5F314");

            entity.HasOne(d => d.TipoPregunta).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.TipoPreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PREGUNTA__TipoPr__00AA174D");
        });

        modelBuilder.Entity<Privacidad>(entity =>
        {
            entity.HasKey(e => e.PrivacidadId).HasName("PK__PRIVACID__1C53A4399FA02DEE");

            entity.ToTable("PRIVACIDAD");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Externo).HasDefaultValue(false);
            entity.Property(e => e.NombrePrivacidad).HasMaxLength(100);
        });

        modelBuilder.Entity<PrivacidadUo>(entity =>
        {
            entity.HasKey(e => e.PrivacidadUoId).HasName("PK__PRIVACID__262E4A48655D440A");

            entity.ToTable("PRIVACIDAD_UO");

            entity.Property(e => e.TipoEntidad).HasMaxLength(20);

            entity.HasOne(d => d.Privacidad).WithMany(p => p.PrivacidadUos)
                .HasForeignKey(d => d.PrivacidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PRIVACIDA__Priva__63A3C44B");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.PuestoId).HasName("PK__PUESTO__F7F6C60441E63A39");

            entity.ToTable("PUESTO");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.NombrePuesto)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Puesto");
        });

        modelBuilder.Entity<RecursoEmbebido>(entity =>
        {
            entity.HasKey(e => e.EmbebidoId).HasName("PK__RECURSO___B550B08C7FF31027");

            entity.ToTable("RECURSO_EMBEBIDO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoEmbebidos)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoEmbebido_ModuloRecurso");
        });

        modelBuilder.Entity<RecursoEncuestum>(entity =>
        {
            entity.HasKey(e => e.EncuestaId).HasName("PK__RECURSO___82FD78E85C12F1A6");

            entity.ToTable("RECURSO_ENCUESTA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoEncuesta)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoEncuesta_ModuloRecurso");
        });

        modelBuilder.Entity<RecursoEvaluacion>(entity =>
        {
            entity.HasKey(e => e.EvaluacionId).HasName("PK__RECURSO___99ABA7450C501295");

            entity.ToTable("RECURSO_EVALUACION");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AgregarPonderacion).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreEvaluacion).HasMaxLength(200);
            entity.Property(e => e.Oportunidades).HasDefaultValue(0);
            entity.Property(e => e.PermitirReinicio)
                .HasMaxLength(50)
                .HasDefaultValue("No reiniciar");
            entity.Property(e => e.PreguntasAleatorias).HasDefaultValue(false);
            entity.Property(e => e.PreguntasCorrectasAprobar).HasDefaultValue(0);
            entity.Property(e => e.TiempoHoras).HasDefaultValue(0);
            entity.Property(e => e.TiempoMinutos).HasDefaultValue(0);
            entity.Property(e => e.TipoCalificacionId).HasDefaultValue(1);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoEvaluacions)
                .HasForeignKey(d => d.ModuloRecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECURSO_E__Modul__047AA831");

            entity.HasOne(d => d.TipoCalificacion).WithMany(p => p.RecursoEvaluacions)
                .HasForeignKey(d => d.TipoCalificacionId)
                .HasConstraintName("FK__RECURSO_E__TipoC__056ECC6A");
        });

        modelBuilder.Entity<RecursoEvaluacionPresencial>(entity =>
        {
            entity.HasKey(e => e.EvaluacionPresencialId).HasName("PK__RECURSO___FE1B27C92447B051");

            entity.ToTable("RECURSO_EVALUACION_PRESENCIAL");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AgregarPonderacion).HasDefaultValue(false);
            entity.Property(e => e.ColaboradorSolicitarRevision).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreTitulo).HasMaxLength(100);
            entity.Property(e => e.TipoCalificacionId).HasDefaultValue(1);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoEvaluacionPresencials)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoEvaluacionPresencial_ModuloRecurso");

            entity.HasOne(d => d.TipoCalificacion).WithMany(p => p.RecursoEvaluacionPresencials)
                .HasForeignKey(d => d.TipoCalificacionId)
                .HasConstraintName("FK__RECURSO_E__TipoC__0662F0A3");
        });

        modelBuilder.Entity<RecursoForo>(entity =>
        {
            entity.HasKey(e => e.ForoId).HasName("PK__RECURSO___8499A22F1FA7E3C1");

            entity.ToTable("RECURSO_FORO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AgregarPonderacion).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreForo).HasMaxLength(200);
            entity.Property(e => e.Privacidad)
                .HasMaxLength(50)
                .HasDefaultValue("Público");
            entity.Property(e => e.TipoCalificacionId).HasDefaultValue(1);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoForos)
                .HasForeignKey(d => d.ModuloRecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECURSO_F__Modul__084B3915");

            entity.HasOne(d => d.TipoCalificacion).WithMany(p => p.RecursoForos)
                .HasForeignKey(d => d.TipoCalificacionId)
                .HasConstraintName("FK__RECURSO_F__TipoC__093F5D4E");
        });

        modelBuilder.Entity<RecursoLectura>(entity =>
        {
            entity.HasKey(e => e.LecturaId).HasName("PK__RECURSO___B421D4FC4B83C068");

            entity.ToTable("RECURSO_LECTURA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ArchivoPdfpath).HasColumnName("ArchivoPDFPath");
            entity.Property(e => e.ContenidoHtml).HasColumnName("ContenidoHTML");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HacerVisibleDashboard).HasDefaultValue(false);
            entity.Property(e => e.NombreArchivoPdf)
                .HasMaxLength(200)
                .HasColumnName("NombreArchivoPDF");

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoLecturas)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoLectura_ModuloRecurso");

            entity.HasOne(d => d.TipoLectura).WithMany(p => p.RecursoLecturas)
                .HasForeignKey(d => d.TipoLecturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecursoLectura_TipoLectura");
        });

        modelBuilder.Entity<RecursoScorm>(entity =>
        {
            entity.HasKey(e => e.ScormId).HasName("PK__RECURSO___CD20EE53C2F1F5C3");

            entity.ToTable("RECURSO_SCORM");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AgregarPonderacion).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(200);
            entity.Property(e => e.PermitirModoPantallaCompleta).HasDefaultValue(true);
            entity.Property(e => e.TamañoMb)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TamañoMB");
            entity.Property(e => e.TipoCalificacionId).HasDefaultValue(1);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoScorms)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoScorm_ModuloRecurso");

            entity.HasOne(d => d.TipoCalificacion).WithMany(p => p.RecursoScorms)
                .HasForeignKey(d => d.TipoCalificacionId)
                .HasConstraintName("FK__RECURSO_S__TipoC__0C1BC9F9");
        });

        modelBuilder.Entity<RecursoSesionPresencial>(entity =>
        {
            entity.HasKey(e => e.SesionPresencialId).HasName("PK__RECURSO___3FE9E449BE3C1B5C");

            entity.ToTable("RECURSO_SESION_PRESENCIAL");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaSesion).HasColumnType("datetime");
            entity.Property(e => e.Lugar).HasMaxLength(200);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoSesionPresencials)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoSesionPresencial_ModuloRecurso");
        });

        modelBuilder.Entity<RecursoTarea>(entity =>
        {
            entity.HasKey(e => e.TareaId).HasName("PK__RECURSO___5CD839918C41A486");

            entity.ToTable("RECURSO_TAREA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AgregarPonderacion).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Privacidad)
                .HasMaxLength(50)
                .HasDefaultValue("Privado");
            entity.Property(e => e.TipoCalificacionId).HasDefaultValue(1);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoTareas)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoTarea_ModuloRecurso");

            entity.HasOne(d => d.TipoCalificacion).WithMany(p => p.RecursoTareas)
                .HasForeignKey(d => d.TipoCalificacionId)
                .HasConstraintName("FK__RECURSO_T__TipoC__0EF836A4");
        });

        modelBuilder.Entity<RecursoVideo>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PK__RECURSO___BAE5126AE1ED5721");

            entity.ToTable("RECURSO_VIDEO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HacerVisibleDashboard).HasDefaultValue(false);
            entity.Property(e => e.TipoSubida).HasMaxLength(20);

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoVideos)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoVideo_ModuloRecurso");
        });

        modelBuilder.Entity<RecursoZoom>(entity =>
        {
            entity.HasKey(e => e.ZoomId).HasName("PK__RECURSO___BF45CA10E8D9C6C7");

            entity.ToTable("RECURSO_ZOOM");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.RecursoZooms)
                .HasForeignKey(d => d.ModuloRecursoId)
                .HasConstraintName("FK_RecursoZoom_ModuloRecurso");
        });

        modelBuilder.Entity<RegistroPrevio>(entity =>
        {
            entity.HasKey(e => e.PrevioId).HasName("PK__REGISTRO__1997DA7B9506FCC2");

            entity.ToTable("REGISTRO_PREVIO");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.DuracionCurso).HasMaxLength(100);
            entity.Property(e => e.ImagenPath).HasMaxLength(1000);
            entity.Property(e => e.NombreCurso).HasMaxLength(100);
            entity.Property(e => e.Recordatorio).HasDefaultValue(true);

            entity.HasOne(d => d.Instructor).WithMany(p => p.RegistroPrevios)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK__REGISTRO___Instr__12C8C788");
        });

        modelBuilder.Entity<Retroalimentacion>(entity =>
        {
            entity.HasKey(e => e.RetroId).HasName("PK__RETROALI__8D9612C193F43A48");

            entity.ToTable("RETROALIMENTACION");
        });

        modelBuilder.Entity<Rubrica>(entity =>
        {
            entity.HasKey(e => e.RubricaId).HasName("PK__RUBRICA__D5D1E23576A06B2A");

            entity.ToTable("RUBRICA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Visible).HasDefaultValue(true);

            entity.HasOne(d => d.EvaluacionPresencial).WithMany(p => p.Rubricas)
                .HasForeignKey(d => d.EvaluacionPresencialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Rubrica_EvaluacionPresencial");
        });

        modelBuilder.Entity<RubricaCalificacion>(entity =>
        {
            entity.HasKey(e => e.CalificacionRubricaId).HasName("PK__RUBRICA___4B31BD9B84EAC8EA");

            entity.ToTable("RUBRICA_CALIFICACION");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Puntos).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Criterio).WithMany(p => p.RubricaCalificacions)
                .HasForeignKey(d => d.CriterioId)
                .HasConstraintName("FK_RubricaCalificacion_Criterio");
        });

        modelBuilder.Entity<RubricaCriterio>(entity =>
        {
            entity.HasKey(e => e.CriterioId).HasName("PK__RUBRICA___11080FC60CA2CAF3");

            entity.ToTable("RUBRICA_CRITERIO");

            entity.HasIndex(e => e.RubricaId, "IX_RubricaCriterio_Rubrica");

            entity.Property(e => e.TituloCriterio).HasMaxLength(200);

            entity.HasOne(d => d.Rubrica).WithMany(p => p.RubricaCriterios)
                .HasForeignKey(d => d.RubricaId)
                .HasConstraintName("FK_RubricaCriterio_Rubrica");
        });

        modelBuilder.Entity<RutaAprendizaje>(entity =>
        {
            entity.HasKey(e => e.RutaId).HasName("PK__RUTA_APR__7B61998E38CD16B1");

            entity.ToTable("RUTA_APRENDIZAJE");

            entity.Property(e => e.CondicionAvanceCurso).HasDefaultValue(false);
            entity.Property(e => e.CondicionAvanceSeccion).HasDefaultValue(false);
            entity.Property(e => e.CriterioAprobacion).HasMaxLength(5);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Gamificacion).HasMaxLength(100);
            entity.Property(e => e.NombreRuta).HasMaxLength(100);

            entity.HasOne(d => d.AsignarTemporalNavigation).WithMany(p => p.RutaAprendizajes)
                .HasForeignKey(d => d.AsignarTemporal)
                .HasConstraintName("FK__RUTA_APRE__Asign__1699586C");

            entity.HasOne(d => d.Certificado).WithMany(p => p.RutaAprendizajes)
                .HasForeignKey(d => d.CertificadoId)
                .HasConstraintName("FK__RUTA_APRE__Certi__178D7CA5");

            entity.HasOne(d => d.Configuracion).WithMany(p => p.RutaAprendizajes)
                .HasForeignKey(d => d.ConfiguracionId)
                .HasConstraintName("FK__RUTA_APRE__Confi__1881A0DE");

            entity.HasOne(d => d.Privacidad).WithMany(p => p.RutaAprendizajes)
                .HasForeignKey(d => d.PrivacidadId)
                .HasConstraintName("FK__RUTA_APRE__Priva__1975C517");
        });

        modelBuilder.Entity<RutaCurso>(entity =>
        {
            entity.HasKey(e => e.RutaCursoId).HasName("PK__RUTA_CUR__110AEAF93224347F");

            entity.ToTable("RUTA_CURSO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasDefaultValue(1);

            entity.HasOne(d => d.Ruta).WithMany(p => p.RutaCursos)
                .HasForeignKey(d => d.RutaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RUTA_CURS__RutaI__1A69E950");

            entity.HasOne(d => d.Seccion).WithMany(p => p.RutaCursos)
                .HasForeignKey(d => d.SeccionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__RUTA_CURS__Secci__1B5E0D89");
        });

        modelBuilder.Entity<SeccionCurso>(entity =>
        {
            entity.HasKey(e => e.SeccionCursoId).HasName("PK__SECCION___4D81B27E1317B7BF");

            entity.ToTable("SECCION_CURSO");

            entity.Property(e => e.Orden).HasDefaultValue(1);

            entity.HasOne(d => d.Seccion).WithMany(p => p.SeccionCursos)
                .HasForeignKey(d => d.SeccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SECCION_C__Secci__5090EFD7");
        });

        modelBuilder.Entity<SeccionRutum>(entity =>
        {
            entity.HasKey(e => e.SeccionId).HasName("PK__SECCION___18B616415C4731E7");

            entity.ToTable("SECCION_RUTA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreSeccion).HasMaxLength(200);
            entity.Property(e => e.Orden).HasDefaultValue(1);

            entity.HasOne(d => d.Ruta).WithMany(p => p.SeccionRuta)
                .HasForeignKey(d => d.RutaId)
                .HasConstraintName("FK__SECCION_R__RutaI__1C5231C2");
        });

        modelBuilder.Entity<SistemaConfiguracion>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("PK__SistemaC__C3BC335CE4EE3887");

            entity.ToTable("SistemaConfiguracion");

            entity.HasIndex(e => e.KeyName, "UQ__SistemaC__F0A2A337C2AD155D").IsUnique();

            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.EsMaestro).HasDefaultValue(false);
            entity.Property(e => e.EsNuevo).HasDefaultValue(false);
            entity.Property(e => e.Estado).HasDefaultValue(false);
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.KeyName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Label)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TareaEntrega>(entity =>
        {
            entity.HasKey(e => e.EntregaId).HasName("PK__TAREA_EN__D9AD2303BFE67AF1");

            entity.ToTable("TAREA_ENTREGA");

            entity.HasIndex(e => e.TareaId, "IX_TareaEntrega_Tarea");

            entity.Property(e => e.Calificacion).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaCalificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaEntrega)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CalificadoPor).WithMany(p => p.TareaEntregaCalificadoPors)
                .HasForeignKey(d => d.CalificadoPorId)
                .HasConstraintName("FK_TareaEntrega_Calificador");

            entity.HasOne(d => d.Tarea).WithMany(p => p.TareaEntregas)
                .HasForeignKey(d => d.TareaId)
                .HasConstraintName("FK_TareaEntrega_Tarea");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TareaEntregaUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TareaEntrega_Usuario");
        });

        modelBuilder.Entity<Tema>(entity =>
        {
            entity.HasKey(e => e.TemaId).HasName("PK__TEMA__BF02E6F66E613BAB");

            entity.ToTable("TEMA");

            entity.Property(e => e.CreacionSubtema).HasMaxLength(500);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.NombreTema).HasMaxLength(200);
        });

        modelBuilder.Entity<TemporalRutum>(entity =>
        {
            entity.HasKey(e => e.TemporalId).HasName("PK__TEMPORAL__DDA59D819E06CE9F");

            entity.ToTable("TEMPORAL_RUTA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AsignarDia).HasMaxLength(100);
            entity.Property(e => e.AsignarFecha).HasColumnType("datetime");
            entity.Property(e => e.FechaLimite).HasDefaultValue(false);
        });

        modelBuilder.Entity<TipoCalificacion>(entity =>
        {
            entity.HasKey(e => e.CalificaionId).HasName("PK__TIPO_CAL__5741FE973E2D6106");

            entity.ToTable("TIPO_CALIFICACION");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<TipoCampo>(entity =>
        {
            entity.HasKey(e => e.CampoId).HasName("PK__TIPO_CAM__2982505638E1D29C");

            entity.ToTable("TIPO_CAMPO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.NombreCampo).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoLectura>(entity =>
        {
            entity.HasKey(e => e.TipoLecturaId).HasName("PK__TIPO_LEC__D991AA71A95F320D");

            entity.ToTable("TIPO_LECTURA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.NombreTipo).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoPreguntum>(entity =>
        {
            entity.HasKey(e => e.TipoPreguntaId).HasName("PK__TIPO_PRE__111B3ED67C2D98A0");

            entity.ToTable("TIPO_PREGUNTA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.NombreTipo).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoRecurso>(entity =>
        {
            entity.HasKey(e => e.RecursoId).HasName("PK__TIPO_REC__82F2B184DF0EC90D");

            entity.ToTable("TIPO_RECURSO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Categoria).HasMaxLength(20);
            entity.Property(e => e.Icono).HasMaxLength(50);
            entity.Property(e => e.NombreTipo).HasMaxLength(50);
        });

        modelBuilder.Entity<UnidadesOrganizacionale>(entity =>
        {
            entity.HasKey(e => e.OrganizacionalesId).HasName("PK__Unidades__8DB304259DA991EE");

            entity.ToTable("Unidades_Organizacionales");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Jefe).WithMany(p => p.UnidadesOrganizacionales)
                .HasForeignKey(d => d.JefeId)
                .HasConstraintName("FK__Unidades___JefeI__2022C2A6");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__USUARIOS__2B3DE7B817435DCF");

            entity.ToTable("USUARIOS");

            entity.HasIndex(e => e.Correo, "UQ__USUARIOS__60695A19BFE02694").IsUnique();

            entity.HasIndex(e => e.KeycloakId, "UQ__USUARIOS__F621404D7444B1CE").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ApeLlido)
                .HasMaxLength(100)
                .HasColumnName("ApeLLido");
            entity.Property(e => e.ContraseñaHash).HasMaxLength(500);
            entity.Property(e => e.Correo).HasMaxLength(300);
            entity.Property(e => e.CorreoAlternativo).HasMaxLength(100);
            entity.Property(e => e.Curp).HasMaxLength(18);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEmpleado).HasMaxLength(100);
            entity.Property(e => e.ImagenPortada).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.RazonSocial).HasMaxLength(100);

            entity.HasOne(d => d.Jefe).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.JefeId)
                .HasConstraintName("FK__USUARIOS__JefeId__25DB9BFC");

            entity.HasOne(d => d.NivelPermiso).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.NivelPermisoId)
                .HasConstraintName("FK__USUARIOS__NivelP__26CFC035");

            entity.HasOne(d => d.Organizacionales).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.OrganizacionalesId)
                .HasConstraintName("FK__USUARIOS__Organi__27C3E46E");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PuestoId)
                .HasConstraintName("FK__USUARIOS__Puesto__28B808A7");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__USUARIOS__RolId__24E777C3");
        });

        modelBuilder.Entity<UsuarioCurso>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__USUARIO___168316B9E0419DA7");

            entity.ToTable("USUARIO_CURSO");

            entity.HasIndex(e => new { e.UsuarioId, e.CursoId }, "UQ_Usuario_Curso").IsUnique();

            entity.Property(e => e.CalificacionFinal).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EsCompletado).HasDefaultValue(false);
            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Progreso)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Curso).WithMany(p => p.UsuarioCursos)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_C__Curso__2116E6DF");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioCursos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_C__Usuar__220B0B18");
        });

        modelBuilder.Entity<UsuarioRecursoProgreso>(entity =>
        {
            entity.HasKey(e => e.ProgresoId).HasName("PK__USUARIO___B9EABD664C14E091");

            entity.ToTable("USUARIO_RECURSO_PROGRESO");

            entity.HasIndex(e => new { e.UsuarioId, e.ModuloRecursoId }, "UQ_Usuario_Recurso").IsUnique();

            entity.Property(e => e.FechaCompletado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ModuloRecurso).WithMany(p => p.UsuarioRecursoProgresos)
                .HasForeignKey(d => d.ModuloRecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_R__Modul__22FF2F51");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRecursoProgresos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_R__Usuar__23F3538A");
        });

        modelBuilder.Entity<UsuarioValor>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.PropiedadId }).HasName("PK__UsuarioV__E6766BBEC62910FC");

            entity.ToTable("UsuarioValor");

            entity.HasOne(d => d.Propiedad).WithMany(p => p.UsuarioValors)
                .HasForeignKey(d => d.PropiedadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioVa__Propi__29AC2CE0");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioValors)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioVa__Usuar__2AA05119");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
