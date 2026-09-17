using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar.Application.Services
{
    public class FormularioService : IFormularioService
    {
        private readonly IFormularioRepository _repo;

        public FormularioService(IFormularioRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<FormularioResultDTO>> CrearFormulario(CrearFormularioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                return OperationResult<FormularioResultDTO>.Fail("El título es obligatorio");

            if (dto.Preguntas == null || !dto.Preguntas.Any())
                return OperationResult<FormularioResultDTO>.Fail("Debe agregar al menos una pregunta");

            return await _repo.CrearFormulario(dto);
        }

        public async Task<OperationResult<FormularioResultDTO>> GuardarRespuestas(EnvioFormularioDTO dto)
        {
            var resultaso = await _repo.GuardarRespuestas(dto);

            return resultaso;
        }

        public async Task<OperationResult<List<FormularioListaDTO>>> ListarFormularios()
        {
            return await _repo.ListarFormularios();
        }

        public async Task<OperationResult<List<FormularioListaDTO>>> ListarFormulariosConRespuestas()
        {
            var resultado = await _repo.ListarFormulariosConRespuestas();

            return resultado;
        }

        public async Task<OperationResult<FormularioResultDTO>> ModificarFormulario(int plantillaId, CrearFormularioDTO dto)
        {
            if (plantillaId <= 0) return OperationResult<FormularioResultDTO>.Fail("ID de plantilla no válido");

            var resultado = await _repo.ModificarFormulario(plantillaId, dto);

            return resultado;
        }

        public async Task<OperationResult<FormularioDetalleDTO?>> ObtenerFormularioPorId(int plantillaId)
        {
            if (plantillaId <= 0)
                return OperationResult<FormularioDetalleDTO?>.Fail("ID no válido");

            return await _repo.ObtenerFormularioPorId(plantillaId);
        }

        public Task<OperationResult<FormularioDetalleDTO?>> ObtenerFormularioPorPublicId(Guid publicId)
        {
            var resultado = _repo.ObtenerFormularioPorPublicId(publicId);

            return resultado;
        }

        public async Task<OperationResult<FormularioRespuestasDTO?>> ObtenerRespuestasFormulario(int plantillaId)
        {
            var resultado = await _repo.ObtenerRespuestasFormulario(plantillaId);

            return resultado;
        }

        public async Task<OperationResult<FormularioResultDTO>> PublicarFormulario(int plantillaId, bool publicar)
        {
            if (plantillaId <= 0) return OperationResult<FormularioResultDTO>.Fail("ID de plantilla no válido");

            var resultado = await _repo.PublicarFormulario(plantillaId, publicar);

            return resultado;
        }
    }
}
