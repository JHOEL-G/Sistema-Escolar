using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class GestionCursoResponseDTO : GestionCursoBaseDTO
    {
        public string? DescripcionCurso { get; set; }
        public string? ImagenPortadaPath { get; set; }
        public string? DuracionCurso { get; set; }
        public bool EstaPublicado { get; set; }
    }
}
