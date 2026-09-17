using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ListarEntregarTareaDTO
    {
        public int TareaId { get; set; }
        public int UsuarioId { get; set; }
        public string? Titulo { get; set; }
        public string? Comentario { get; set; }
        public string? Archivo { get; set; }     
        public string? ArchivoPath { get; set; }  
        public decimal? Calificacion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public int? CalificadoPorId { get; set; }
        public int? EntregaId { get; set; }
    }
}
