using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IRegistroPrevioService
    {
        Task<OperationResult> CrearRegistroPrevio(RegistroPrevioDTO registroPrevioDto, Stream? imagenStream = null, string? nombreImagen = null);
        Task<OperationResult> EditarRegistroPrevio(RegistroPrevioDTO registroPrevioDto);
        Task<OperationResult<IEnumerable<RegistroPrevioDTO>>> ObtenerRegistrosPrevios();
        Task<OperationResult<RegistroPrevioDTO?>> ObtenerRegistroPrevioPorId(int id);
    }
}
