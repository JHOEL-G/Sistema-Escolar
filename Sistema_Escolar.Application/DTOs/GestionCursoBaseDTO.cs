using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class GestionCursoBaseDTO
    {
        public int GestionCursoId { get; set; }
        public int CursoId { get; set; }
        public string NombreCurso { get; set; } = string.Empty;
        public string Privacidad { get; set; } = "Privado";
        public bool InscripcionAutomatica { get; set; }
        public bool PermitirDesinscripcion { get; set; }
        public bool Gamificacion { get; set; }
        public int CreadoPor { get; set; }
        public string? TemasJSON { get; set; }
        public string? ParticipantesJSON { get; set; }
        public string? EvaluadoresJSON { get; set; }
        public string? CriteriosJSON { get; set; }
        public string? VisibilidadJSON { get; set; }

        [NotMapped]
        public List<VisibilidadGrupoDTO>? Visibilidad { get; set; }

        [NotMapped]
        public List<TemaSeleccionadoDTO> Temas { get; set; } = new();

        [NotMapped]
        public List<int> Participantes { get; set; } = new();

        [NotMapped]
        public List<int> Evaluadores { get; set; } = new();

        [NotMapped]
        public List<CriterioInscripcionDTO> Criterios { get; set; } = new();
    }
}
