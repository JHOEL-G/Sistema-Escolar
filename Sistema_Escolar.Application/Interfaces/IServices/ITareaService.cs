using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface ITareaService
    {
        Task<OperationResult> CrearTarea(RecursoTareaDTO dto, List<(Stream Stream, string NombreArchivo)>? archivos = null);

        string GenerarPresignedDataJson(string jsonRaw);
     
        Task<OperationResult> EntregarTarea(EntregarTareaDTO dto, Stream? archivoStream = null, string? nombreArchivo = null);
        Task<OperationResult> ListarTareaEntregados(int tarea, int usuario);
    }
}
