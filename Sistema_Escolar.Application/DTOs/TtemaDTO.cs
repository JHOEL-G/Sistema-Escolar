using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class TtemaDTO
    {
        public int TemaId { get; set; }

        public string? NombreTema { get; set; }

        public string? Descripcion { get; set; }

        public string? ImagenPortada { get; set; }
        public string? CreacionSubtema { get; set; }

        [NotMapped]
        public List<string> CreacionSubtemaList
        {
            get => string.IsNullOrEmpty(CreacionSubtema)
                   ? new List<string>()
                   : CreacionSubtema.Split(',').Select(s => s.Trim()).ToList();
        }
    }
}
