    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Sistema_Escolar.Infrastructure.DTOs.StoreProcedure
    {
        public class StoreProcedureResult
        {
        public int? UsuarioId { get; set; }
        public int? ResultId { get; set; }

        public string Mensaje { get; set; } = string.Empty;
    }
}
