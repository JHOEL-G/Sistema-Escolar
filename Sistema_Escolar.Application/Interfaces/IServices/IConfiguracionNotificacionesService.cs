using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IConfiguracionNotificacionesService
    {
        Task<OperationResult<IEnumerable<ConfiguracionModuloDTO>>> ListarConfiguracionByCategoria(string categoria);
        Task<OperationResult> AlterarConfiguracion(UpdateConfiguracionRequestDTO dto);
    }
}
