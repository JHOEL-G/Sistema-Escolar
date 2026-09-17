using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services.AwsService
{
    public class LecturaService : ILecturaService
    {
        private readonly IRecursoLecturaRepository _repo;
        private readonly IFileStorageService _service;

        public LecturaService (IRecursoLecturaRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearLectura(RecursoLecturaDTO dTO, Stream? archivoStream = null, string? nombreArchivo = null, List<(Stream stream, string nombre)>? archivosAdjuntos = null)
        {
            if (archivoStream != null && !string.IsNullOrEmpty(nombreArchivo))
                dTO.ArchivoPDFPath = await _service.UploadFile(archivoStream, nombreArchivo, "archivo-lectura");
            else
                dTO.ArchivoPDFPath = null;

            if (archivosAdjuntos != null && archivosAdjuntos.Any() && dTO.ArchivosAdjuntos != null)
            {
                foreach (var adjunto in dTO.ArchivosAdjuntos)
                {
                    var archivoMatch = archivosAdjuntos.FirstOrDefault(a =>
                        Path.GetFileName(a.nombre) == Path.GetFileName(adjunto.NombreArchivo));

                    if (archivoMatch != default)
                    {
                        adjunto.RutaArchivo = await _service.UploadFile(
                            archivoMatch.stream, archivoMatch.nombre, "archivo-adjunto");
                    }
                }
            }

            return await _repo.CreateRecursoLectura(dTO);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var lectura = JsonSerializer.Deserialize<RecursoLecturaDTO>(jsonRaw, options);
            if (lectura == null) return jsonRaw;

            bool modificado = false;

            if (!string.IsNullOrEmpty(lectura.ArchivoPDFPath))
            {
                lectura.ArchivoPDFPath = _service.GetPresignedUrl(lectura.ArchivoPDFPath, 120);
                modificado = true;
            }

            foreach (var adjunto in lectura.ArchivosAdjuntos ?? new List<ArchivoAdjuntoDTO>())
            {
                if (!string.IsNullOrEmpty(adjunto.RutaArchivo) && !adjunto.RutaArchivo.StartsWith("http"))
                {
                    adjunto.RutaArchivo = _service.GetPresignedUrl(adjunto.RutaArchivo, 120);
                    modificado = true;
                }
            }

            return modificado ? JsonSerializer.Serialize(lectura, options) : jsonRaw;
        }
    }
}
