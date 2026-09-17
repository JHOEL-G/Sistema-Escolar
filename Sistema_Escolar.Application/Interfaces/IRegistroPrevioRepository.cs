using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IRegistroPrevioRepository
    {
        Task<IEnumerable<RegistroPrevioDTO>> GetRegistroPrevios();
        Task<RegistroPrevioDTO?> GetRegistroPrevioById(int id);
        Task<OperationResult> CreateRegistroPrevio(RegistroPrevioDTO registroPrevioDto);
        Task<OperationResult> UpdateRegistroPrevio(RegistroPrevioDTO registroPrevioDto);
    }
}
