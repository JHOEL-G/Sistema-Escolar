using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RegistroPrevioDTO
    {
        public int PrevioId { get; set; }

        public string? NombreCurso { get; set; }

        public string? Descripcion { get; set; }

        public string? ImagenPath { get; set; }

        public string? DuracionCurso { get; set; }

        public string? MensajeBienvenida { get; set; }

        public int? InstructorId { get; set; }

        public bool? Recordatorio { get; set; }
    }
}
