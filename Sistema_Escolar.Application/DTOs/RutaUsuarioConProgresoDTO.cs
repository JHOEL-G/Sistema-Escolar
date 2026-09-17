using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class RutaUsuarioConProgresoDTO
    {
        public IEnumerable<RutaUsuarioDTO> Rutas { get; set; } = new List<RutaUsuarioDTO>();
        public IEnumerable<CursoProgresoDTO> CursosProgreso { get; set; } = new List<CursoProgresoDTO>();
    }
}
