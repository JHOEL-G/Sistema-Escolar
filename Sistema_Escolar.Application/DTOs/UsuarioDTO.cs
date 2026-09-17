using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public Guid? KeycloakId { get; set; }
        public string? Nombre { get; set; }
        public string? ApeLLido { get; set; }
        public string Correo { get; set; } = string.Empty;

        public string? Contraseña { get; set; } = string.Empty;

        public string? CorreoAlternativo { get; set; }
        public string? Curp { get; set; }
        public string? IdEmpleado { get; set; }

        public string? RazonSocial { get; set; }

        public DateTime? FechaActivacion { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public int? RolId { get; set; }
        public int? PuestoId { get; set; }
        public int? OrganizacionalesId { get; set; }
        public int? JefeId { get; set; }

        public int? NivelPermisoId { get; set; }
        public string? PermisoNombre { get; set; }
        public string? Puesto { get; set; }
        public string? Unidad_Organizacional { get; set; }
        public string? NombreOu { get; set; }       
        public string? NombreRol { get; set; }
        public string? Nombre_Jefe { get; set; }
        public string? FotoNombre { get; set; }
        public string? ImagenPortada { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public bool? Activo { get; set; }
    }
}
