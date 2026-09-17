using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class CursoDetalleDTO
    {
        public int CursoId { get; set; }
        public string? NombreCurso { get; set; }
        public string? DescripcionCurso { get; set; }
        public string? ImagenCurso { get; set; }
        public int OrdenCurso { get; set; }
    }
}
