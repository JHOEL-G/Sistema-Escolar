using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class ModuloDTO
    {
        public int ModuloId { get; set; }
        public int CursoId { get; set; }
        private int _ordenModulo;
        public int OrdenModulo
        {
            get => _ordenModulo;
            set => _ordenModulo = value;
        }
        public int Orden
        {
            get => _ordenModulo;
            set => _ordenModulo = value;
        }

        public string? ModuloTitulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
