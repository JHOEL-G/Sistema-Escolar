using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class BancaPreguntaDTO
    {
        public int BancaId { get; set; }
        public string NombreBanca { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string ArchivoPlantilla { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int TotalPreguntas { get; set; }
        public List<PreguntaCrearDTO> Preguntas { get; set; } = new();
    }
}
