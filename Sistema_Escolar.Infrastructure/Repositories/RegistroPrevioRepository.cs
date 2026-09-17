using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.DTOs.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class RegistroPrevioRepository : IRegistroPrevioRepository
    {
        private readonly ConfiaContext _context;

        public RegistroPrevioRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateRegistroPrevio(RegistroPrevioDTO registroPrevioDto)
        {
            var parametros = new[]
            {
                new SqlParameter("@NombreCurso", registroPrevioDto.NombreCurso),
                new SqlParameter("@Descripcion", registroPrevioDto.Descripcion),
                new SqlParameter("@ImagenPath", registroPrevioDto.ImagenPath ?? (object)DBNull.Value),
                new SqlParameter("@DuracionCurso", registroPrevioDto.DuracionCurso),
                new SqlParameter("@MensajeBienvenida", registroPrevioDto.MensajeBienvenida),
                new SqlParameter("@InstructorId", registroPrevioDto.InstructorId),
                new SqlParameter("@Recordatorio", registroPrevioDto.Recordatorio)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_Crear_Registro_Previo @NombreCurso, @Descripcion, @ImagenPath, @DuracionCurso, @MensajeBienvenida, @InstructorId, @Recordatorio", 
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al ejecutar el procedimiento almacenado.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }

        public async Task<RegistroPrevioDTO?> GetRegistroPrevioById(int id)
        {
            var resultado = await _context.Database.SqlQueryRaw<RegistroPrevioDTO>(
                "EXEC sp_Obtener_Registro_Id @PrevioId",
                new SqlParameter("@PrevioId", id)).ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<RegistroPrevioDTO>> GetRegistroPrevios()
        {
            var resltado = await _context.Database.SqlQueryRaw<RegistroPrevioDTO>(
                "EXEC sp_ObtenerTodoRegistro").ToListAsync();

            return resltado;
        }

        public async Task<OperationResult> UpdateRegistroPrevio(RegistroPrevioDTO registroPrevioDto)
        {
            var parametros = new[]
            {
                new SqlParameter("@PrevioId", registroPrevioDto.PrevioId),
                new SqlParameter("@NombreCurso", registroPrevioDto.NombreCurso),
                new SqlParameter("@Descripcion", registroPrevioDto.Descripcion),
                new SqlParameter("@ImagenPath", registroPrevioDto.ImagenPath),
                new SqlParameter("@DuracionCurso", registroPrevioDto.DuracionCurso),
                new SqlParameter("@MensajeBienvenida", registroPrevioDto.MensajeBienvenida),
                new SqlParameter("@InstructorId", registroPrevioDto.InstructorId),
                new SqlParameter("@Recordatorio", registroPrevioDto.Recordatorio)
            };

            var resultado = await _context.Database.SqlQueryRaw<StoreProcedureResult>(
                "EXEC sp_Modificar_Registro @PrevioId, @NombreCurso, @Descripcion, @ImagenPath, @DuracionCurso, @MensajeBienvenida, @InstructorId, @Recordatorio", 
                parametros).ToListAsync();

            var sp = resultado.FirstOrDefault();

            if (sp == null) return OperationResult.Fail("Error al ejecutar el procedimiento almacenado.");

            return sp.ResultId > 0
                ? OperationResult.Ok(sp.Mensaje)
                : OperationResult.Fail(sp.Mensaje);
        }
    }
}
