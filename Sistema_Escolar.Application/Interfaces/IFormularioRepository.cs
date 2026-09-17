using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IFormularioRepository
    {
        Task<OperationResult<FormularioResultDTO>> CrearFormulario(CrearFormularioDTO dto);
        Task<OperationResult<FormularioResultDTO>> ModificarFormulario(int plantillaId, CrearFormularioDTO dto);
        Task<OperationResult<FormularioResultDTO>> PublicarFormulario(int plantillaId, bool publicar);
        Task<OperationResult<List<FormularioListaDTO>>> ListarFormularios();
        Task<OperationResult<FormularioDetalleDTO?>> ObtenerFormularioPorId(int plantillaId);
        Task<OperationResult<FormularioDetalleDTO?>> ObtenerFormularioPorPublicId(Guid publicId);
        Task<OperationResult<FormularioResultDTO>> GuardarRespuestas(EnvioFormularioDTO dto);
        Task<OperationResult<FormularioRespuestasDTO?>> ObtenerRespuestasFormulario(int plantillaId);
        Task<OperationResult<List<FormularioListaDTO>>> ListarFormulariosConRespuestas();
    }
}
