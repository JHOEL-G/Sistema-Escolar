using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class ConfiguracionNotificacionesRepository : IConfiguracionNotificacionesRepository
    {
        private readonly ConfiaContext _context; 

        public ConfiguracionNotificacionesRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> ToggleConfiguracion(UpdateConfiguracionRequestDTO dto)
        {
            var parametros = new[]
            {
                new SqlParameter("@KeyName", dto.KeyName),
                new SqlParameter("@NuevoEstado", dto.NuevoEstado)
            };

            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarEstadoConfiguracion @KeyName, @NuevoEstado", parametros);

            return OperationResult.Ok("Configuración actualizada correctamente.");
        }

        public async Task<OperationResult<IEnumerable<ConfiguracionModuloDTO>>> GetConfiguracionByCategoria(string categoria)
        {
            var resultado = await _context.Database
            .SqlQueryRaw<ConfiguracionModuloDTO>(
                "EXEC sp_ObtenerConfiguracionPorCategoria @Categoria",
                new SqlParameter("@Categoria", categoria)
            )
            .ToListAsync();

            return OperationResult<IEnumerable<ConfiguracionModuloDTO>>.Ok(resultado);
        }
    }
}
