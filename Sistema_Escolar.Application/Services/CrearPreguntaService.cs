using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class CrearPreguntaService : ICearPreguntaService
    {
        private readonly ICrearPreguntaRepository _repo;

        public CrearPreguntaService (ICrearPreguntaRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult> ActualizarPregunta(int bancaId, BancaPreguntaDTO dto)
        {
            if (bancaId <= 0)
                return OperationResult.Fail("El ID de la banca no es válido.");

            if (string.IsNullOrWhiteSpace(dto.NombreBanca))
                return OperationResult.Fail("El nombre de la banca es obligatorio.");

            if (dto.Preguntas == null || !dto.Preguntas.Any())
                return OperationResult.Fail("La banca debe contener al menos una pregunta.");

            foreach (var pregunta in dto.Preguntas)
            {
                if (string.IsNullOrWhiteSpace(pregunta.TextoPregunta))
                    return OperationResult.Fail("Hay preguntas con el texto vacío.");

                if (pregunta.Opciones == null || !pregunta.Opciones.Any())
                    return OperationResult.Fail($"La pregunta '{pregunta.TextoPregunta}' no tiene opciones configuradas.");
            }

            return await _repo.UpdatePregunta(bancaId, dto);
        }

        public async Task<OperationResult> CearPregunta(BancaPreguntaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NombreBanca))
                return OperationResult.Fail("El nombre de la banca es obligatorio.");

            if (dto.Preguntas == null || !dto.Preguntas.Any())
                return OperationResult.Fail("La banca debe contener al menos una pregunta.");

            foreach (var pregunta in dto.Preguntas)
            {
                if (string.IsNullOrWhiteSpace(pregunta.TextoPregunta))
                    return OperationResult.Fail("Hay preguntas con el texto vacío.");

                if (pregunta.Opciones == null || !pregunta.Opciones.Any())
                    return OperationResult.Fail($"La pregunta '{pregunta.TextoPregunta}' no tiene opciones configuradas.");
            }

            var resultado = await _repo.CreatePregunta(dto);

            return resultado;
        }

        public async Task<OperationResult<BancaPreguntaDTO?>> ObtenerPreguntaPorId(int bancaId)
        {
            if (bancaId <= 0) return OperationResult<BancaPreguntaDTO?>.Fail("El ID de la banca no existe.");

            var resultado = await _repo.GetPreguntaById(bancaId);

            return resultado != null
                ? OperationResult<BancaPreguntaDTO?>.Ok(resultado)
                : OperationResult<BancaPreguntaDTO?>.Fail("No se encontró la banca con el ID proporcionado.");
        }

        public async Task<OperationResult<IEnumerable<BancaPreguntaDTO>>> ObtenerPreguntas()
        {
            try
            {
                var resultado = await _repo.GetPregunta();

                if (resultado == null || !resultado.Any())
                {
                    return OperationResult<IEnumerable<BancaPreguntaDTO>>.Ok(new List<BancaPreguntaDTO>(), "No se encontraron registros.");
                }

                return OperationResult<IEnumerable<BancaPreguntaDTO>>.Ok(resultado);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<BancaPreguntaDTO>>.Fail($"Error al obtener las preguntas: {ex.Message}");
            }
        }
    }
}
