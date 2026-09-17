using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CursoCreationRequest
    {
        public string? NombreCurso { get; set; }
        public bool HabilitarFechaCurso { get; set; }
        public int? DificultadId { get; set; }
        public int? LenguajeId { get; set; }
        public string? DescripcionCurso { get; set; }

        public List<string>? CaracteristicasQueAprendere { get; set; }
        public List<string>? CaracteristicasHabilidades { get; set; }
        public List<string>? CaracteristicasRequerimientos { get; set; }

        public string? Avance { get; set; }
        public int? RetroalimentacionId { get; set; }
        public string? DuracionCurso { get; set; }
        public string? MensajeBienvenida { get; set; }
        public int? InstructorId { get; set; }
        public bool Reacreditacion { get; set; }
        public bool EstaPublicado { get; set; }

        public List<int>? InstructorIds { get; set; }


        public List<ModuloDTO>? Modulos { get; set; }
        public List<RecursoDTO>? Recursos { get; set; }

        public string? PorQueInscribirmeCurso { get; set; }
        public int? Calificacion { get; set; }
        public int? CursoReacreditacionId { get; set; }
        public int? PeriodoVigencia { get; set; }

    }
}
