using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class ConfiguracionNotificacionesService : IConfiguracionNotificacionesService
    {
        private readonly IConfiguracionNotificacionesRepository _repo;

        public ConfiguracionNotificacionesService(IConfiguracionNotificacionesRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> AlterarConfiguracion(UpdateConfiguracionRequestDTO dto)
        {
            if (string.IsNullOrEmpty(dto.KeyName))
                return OperationResult.Fail("El identificador de configuración (KeyName) es obligatorio.");

            var resultado = await _repo.ToggleConfiguracion(dto);

            return resultado;
        }

        public async Task<OperationResult<IEnumerable<ConfiguracionModuloDTO>>> ListarConfiguracionByCategoria(string categoria)
        {
            if (string.IsNullOrEmpty(categoria))
                return OperationResult<IEnumerable<ConfiguracionModuloDTO>>.Fail("La categoría es requerida.");

            var resultado = await _repo.GetConfiguracionByCategoria(categoria);

            if (!resultado.Success)
                return OperationResult<IEnumerable<ConfiguracionModuloDTO>>.Fail(resultado.Message);

            return OperationResult<IEnumerable<ConfiguracionModuloDTO>>.Ok(resultado.Data, "Configuraciones obtenidas con éxito");
        }
    }
}
