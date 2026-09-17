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
    public class TareaService : ITareaService
    {
        private readonly IRecursoTareaRepository _repo;
        private readonly IFileStorageService _service;  

        public TareaService (IRecursoTareaRepository repo, IFileStorageService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<OperationResult> CrearTarea(RecursoTareaDTO dto, List<(Stream Stream, string NombreArchivo)>? archivos = null)
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
                    var path = await _service.UploadFile(stream, nombreArchivo, "recurso-tarea");
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

            var resultado = await _repo.CreateRecursoTarea(dto);

            return resultado;
        }

        public async Task<OperationResult> EntregarTarea(EntregarTareaDTO dto, Stream? archivoStream = null, string? nombreArchivo = null)
        {
            if (archivoStream != null && !string.IsNullOrEmpty(nombreArchivo))
                dto.Archivo = await _service.UploadFile(archivoStream, nombreArchivo, "entregas-tarea");

            return await _repo.EntregarTarea(dto);
        }

        public string GenerarPresignedDataJson(string jsonRaw)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var tarea = JsonSerializer.Deserialize<RecursoTareaDTO>(jsonRaw, options);
            if (tarea == null) return jsonRaw;

            if (tarea.Archivos != null)
                foreach (var archivo in tarea.Archivos)
                    if (!string.IsNullOrEmpty(archivo.ArchivoPath))
                        archivo.ArchivoPath = _service.GetPresignedUrl(archivo.ArchivoPath, 120);

            return JsonSerializer.Serialize(tarea, options);
        }

        public async Task<OperationResult> ListarTareaEntregados(int tarea, int usuario)
        {
            var resultado = await _repo.GetTareaEntregados(tarea, usuario);
            if (!resultado.Success || resultado.Data == null) return resultado;

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var entrega = JsonSerializer.Deserialize<ListarEntregarTareaDTO>(
                    resultado.Data.ToString()!, options);

                if (entrega != null && !string.IsNullOrEmpty(entrega.ArchivoPath))
                {
                    entrega.ArchivoPath = _service.GetPresignedUrl(entrega.ArchivoPath, 120);
                    return OperationResult.Ok(entrega, "Entrega obtenida");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TareaService] Error: {ex.Message}");
            }

            return resultado;
        }
    }
}
