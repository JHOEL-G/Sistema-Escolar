using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface ITemaService
    {
        Task<OperationResult<IEnumerable<TtemaDTO>>> ObtenerTemas();
        Task<OperationResult> CrearTema(TtemaDTO temaDTO, Stream? imagenStream = null, string? nombreImagen = null);
        Task<OperationResult<IEnumerable<TtemaDTO?>>> ObtenerTemaId(int id);
        Task<OperationResult> ActualizarTema(TtemaDTO temaDTO, Stream? imagenStream = null, string? nombreImagen = null);
    }
}
