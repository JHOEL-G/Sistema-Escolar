using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class InstructorCursoDTO
    {
        public int CursoInstructorId { get; set; }
        public int InstructorId { get; set; }
        public string? NombreInstructor { get; set; }
        public bool EsInstructorPrincipal { get; set; }
        public bool Activo { get; set; }
    }
}
