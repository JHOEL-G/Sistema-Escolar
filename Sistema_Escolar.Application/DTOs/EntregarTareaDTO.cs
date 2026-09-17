using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Sistema_Escolar.Application.DTOs
{
    public class EntregarTareaDTO
    {
        public int TareaId { get; set; }
        public int UsuarioId { get; set; }
        public string? Titulo { get; set; }
        public string? Comentario { get; set; }
        public string? Archivo { get; set; }
    }
}
