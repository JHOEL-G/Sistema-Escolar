using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CrearGestionCursoDTO
    {
        public int? GestionCursoId { get; set; } 
        public int CursoId { get; set; }
        public string NombreCurso { get; set; } = string.Empty;
        public string Privacidad { get; set; } = "Privado";
        public bool InscripcionAutomatica { get; set; }
        public bool PermitirDesinscripcion { get; set; }
        public bool Gamificacion { get; set; }
        public int CreadoPor { get; set; }

        public List<TemaSeleccionadoDTO> Temas { get; set; } = new List<TemaSeleccionadoDTO>();
        public List<int> Participantes { get; set; } = new List<int>();
        public List<int> Evaluadores { get; set; } = new List<int>();
        public List<CriterioInscripcionDTO> Criterios { get; set; } = new List<CriterioInscripcionDTO>();
        public List<VisibilidadGrupoDTO>? Visibilidad { get; set; }


        public string? TemasJSON { get; set; }
        public string? ParticipantesJSON { get; set; }
        public string? EvaluadoresJSON { get; set; }
        public string? CriteriosJSON { get; set; }
    }
}
