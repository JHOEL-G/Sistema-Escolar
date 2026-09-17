using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IConfiguracionNotificacionesRepository
    {
        Task<OperationResult<IEnumerable<ConfiguracionModuloDTO>>> GetConfiguracionByCategoria(string categoria);
         Task<OperationResult> ToggleConfiguracion(UpdateConfiguracionRequestDTO dto);
    }
}
