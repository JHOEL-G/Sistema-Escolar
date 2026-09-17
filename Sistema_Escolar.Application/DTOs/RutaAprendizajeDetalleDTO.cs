using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sistema_Escolar.Application.DTOs
{
    public class RutaAprendizajeDetalleDTO
    {
        public int RutaId { get; set; }
        public string? NombreRuta { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenPortada { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool CondicionAvanceCurso { get; set; }
        public bool CondicionAvanceSeccion { get; set; }
        public string? CriterioAprobacion { get; set; }
        public string? Gamificacion { get; set; }
        public string? MensajeBienvenida { get; set; }

        public int? PrivacidadId { get; set; }
        public string? NombrePrivacidad { get; set; }
        public bool Externo { get; set; }

        public DateTime? AsignarFecha { get; set; }
        public string? AsignarDia { get; set; }
        public bool FechaLimite { get; set; }

        public int? ConfiguracionId { get; set; }
        public int? PropiedadesId { get; set; }
        public bool PermiteDesinscripcion { get; set; }

        public int? CertificadoId { get; set; }
        public string? NombreCertificado { get; set; }

        public int TotalSecciones { get; set; }
        public int TotalCursos { get; set; }
        public int TotalParticipantes { get; set; }

        [JsonIgnore]
        public string? SeccionesJson { get; set; }
        [JsonIgnore]
        public string? CertificadoresJson { get; set; }
        [JsonIgnore]
        public string? ParticipantesJson { get; set; }
        public string? UosPrivacidadIds { get; set; }
        public string? RolesPrivacidadIds { get; set; }
        public string? PropiedadesPrivacidadIds { get; set; }

        public string? UosInscripcionIds { get; set; }
        public string? RolesInscripcionIds { get; set; }

        [NotMapped]
        public List<SeccionDetalleDTO>? Secciones =>
            string.IsNullOrEmpty(SeccionesJson)
                ? null
                : JsonSerializer.Deserialize<List<SeccionDetalleDTO>>(SeccionesJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        [NotMapped]
        public List<CertificadorDetalleDTO>? Certificadores =>
            string.IsNullOrEmpty(CertificadoresJson)
                ? null
                : JsonSerializer.Deserialize<List<CertificadorDetalleDTO>>(CertificadoresJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        [NotMapped]
        public List<int>? ParticipantesIds =>
    string.IsNullOrEmpty(ParticipantesJson)
        ? null
        : JsonSerializer.Deserialize<List<JsonElement>>(ParticipantesJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?.Where(e => e.TryGetProperty("usuarioId", out _))
            .Select(e => e.GetProperty("usuarioId").GetInt32())
            .ToList();
    }
}
