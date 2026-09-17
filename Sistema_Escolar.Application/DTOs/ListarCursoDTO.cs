using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sistema_Escolar.Application.DTOs
{
    public class ListarCursoDTO
    {
        public int CursoId { get; set; }
        public string? NombreCurso { get; set; }
        public bool HabilitarFechaCurso { get; set; }
        public string? ImagenPortadaPath { get; set; }
        public int? DificultadId { get; set; }
        public string? NombreDificultad { get; set; }
        public int? LenguajeId { get; set; }
        public string? NombreLenguaje { get; set; }
        public string? DescripcionCurso { get; set; }
        public string? CaracteristicasQueAprendere { get; set; }
        public string? CaracteristicasHabilidades { get; set; }
        public string? CaracteristicasRequerimientos { get; set; }
        public string? VideoPromocionalPath { get; set; }
        public string? Avance { get; set; }
        public int? RetroalimentacionId { get; set; }
        public string? NombreRetro { get; set; }
        public string? DuracionCurso { get; set; }
        public string? MensajeBienvenida { get; set; }
        public int? InstructorId { get; set; }
        public string? NombreInstructor { get; set; }
        public bool Reacreditacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public bool EstaPublicado { get; set; }
        public int TotalModulos { get; set; }
        public int TotalInscritos { get; set; }
        public int TotalCompletados { get; set; }
        public int TotalRecursos { get; set; }
        public decimal ProgresoPromedio { get; set; }
        public decimal CalificacionPromedio { get; set; }
        public int TotalInstructores { get; set; }
        public int GestionesActivas { get; set; }

        [JsonIgnore]
        public string? Modulos { get; set; }

        [JsonIgnore]
        public string? Instructores { get; set; }

        public List<ModuloDTO>? ModulosParsed { get; set; }
        public List<InstructorCursoDTO>? InstructoresParsed { get; set; }

        public string? PorQueInscribirmeCurso { get; set; }
        public int? Calificacion { get; set; }
        public int? CursoReacreditacionId { get; set; }
        public int? PeriodoVigencia { get; set; }
    }
}
