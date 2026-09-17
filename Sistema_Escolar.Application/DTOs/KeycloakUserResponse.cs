using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.DTOs
{
    public class KeycloakUserResponse
    {
        public string id { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool enabled { get; set; }
        public bool emailVerified { get; set; }
        public long createdTimestamp { get; set; }
        public Dictionary<string, List<string>> attributes { get; set; } = new Dictionary<string, List<string>>();

        public List<string> realmRoles { get; set; } = new List<string>();
        public List<string> groups { get; set; } = new List<string>();
        public List<KeycloakRoleMapping> clientRoles { get; set; } = new List<KeycloakRoleMapping>();

        public string? Matricula => GetFirstAttribute("matricula");
        public string? CURP => GetFirstAttribute("curp");
        public string? TipoUsuario => GetFirstAttribute("tipo_usuario"); 
        public string? Grado => GetFirstAttribute("grado");
        public string? Grupo => GetFirstAttribute("grupo");
        public string? Turno => GetFirstAttribute("turno");
        public string? Carrera => GetFirstAttribute("carrera");
        public string? IdEmpleado => GetFirstAttribute("id_empleado");
        public string? Telefono => GetFirstAttribute("telefono");
        public string? TelefonoEmergencia => GetFirstAttribute("telefono_emergencia");
        public DateTime? FechaNacimiento => GetDateAttribute("fecha_nacimiento");
        public string? Genero => GetFirstAttribute("genero");
        public string? Departamento => GetFirstAttribute("departamento");

        private string? GetFirstAttribute(string key)
        {
            return attributes.ContainsKey(key) && attributes[key].Count > 0
                ? attributes[key][0]
                : null;
        }

        private DateTime? GetDateAttribute(string key)
        {
            var value = GetFirstAttribute(key);
            return DateTime.TryParse(value, out var date) ? date : null;
        }
    }

    public class KeycloakRoleMapping
    {
        public string clientId { get; set; } = string.Empty;
        public List<string> roles { get; set; } = new List<string>();
    }
}