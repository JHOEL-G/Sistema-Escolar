using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class ForoService : IForoService
    {
        private readonly IRecursoForoRepository _repo;
        private readonly IFileStorageService _service;

        public ForoService (IRecursoForoRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearForo(RecursoForoDTO dto, List<(Stream Stream, string NombreArchivo)>? archivos = null)
        {
            dto.ArchivoPath = null;

            var archivosExistentes = dto.Archivos?
                .Where(a => !string.IsNullOrEmpty(a.ArchivoPath))
                .ToList() ?? new List<ArchivoDTO>();

            var archivosSubidos = new List<ArchivoDTO>();

            if (archivos != null && archivos.Count > 0)
            {
                if (archivos.Count > 6)
                    return OperationResult.Fail("Se permite un máximo de 6 archivos.");

                for (int i = 0; i < archivos.Count; i++)
                {
                    var (stream, nombreArchivo) = archivos[i];
                    var path = await _service.UploadFile(stream, nombreArchivo, "recurso-foro");
                    archivosSubidos.Add(new ArchivoDTO
                    {
                        ArchivoPath = path,
                        NombreArchivo = nombreArchivo,
                        TipoArchivo = Path.GetExtension(nombreArchivo).TrimStart('.'),
                        Orden = archivosExistentes.Count + i + 1
                    });
                }
            }

            dto.Archivos = archivosExistentes.Concat(archivosSubidos).ToList();

            return await _repo.CreateRecursoForo(dto);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var foro = JsonSerializer.Deserialize<RecursoForoDTO>(jsonRaw, options);
            if (foro == null) return jsonRaw;

            if (foro.Archivos != null)
                foreach (var archivo in foro.Archivos)
                    if (!string.IsNullOrEmpty(archivo.ArchivoPath))
                        archivo.ArchivoPath = _service.GetPresignedUrl(archivo.ArchivoPath, 120);

            return JsonSerializer.Serialize(foro, options);
        }

        public async Task<OperationResult> ListarForoPublicado(int foro)
        {
            var resultado = await _repo.GetForosPublicado(foro);
            if (!resultado.Success || resultado.Data == null) return resultado;

            try
            {
                var publicaciones = resultado.Data as List<PublicarForoDTO>;
                if (publicaciones == null) return resultado;

                foreach (var pub in publicaciones)
                    if (!string.IsNullOrEmpty(pub.ArchivoPath))
                        pub.ArchivoPath = _service.GetPresignedUrl(pub.ArchivoPath, 120);

                return OperationResult.Ok(publicaciones, "Publicaciones obtenidas");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ForoService] Error: {ex.Message}");
            }

            return resultado;
        }

        public async Task<OperationResult> PublicarForo(PublicarForoDTO dto, Stream? archivoStream = null, string? nombreArchivo = null)
        {
            if (archivoStream != null && !string.IsNullOrEmpty(nombreArchivo))
                dto.ArchivoPath = await _service.UploadFile(archivoStream, nombreArchivo, "publicaciones-foro");

            return await _repo.PublicarForo(dto);
        }
    }
}
