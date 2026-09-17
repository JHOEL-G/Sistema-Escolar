using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces.IServices
{
    public interface IGestionCursoService
    {
        Task<OperationResult> CrearGestionCurso(CrearGestionCursoDTO dto);

        Task<OperationResult<IEnumerable<GestionCursoBaseDTO>>> GetIdGestiomCurso(int id);

        Task<OperationResult<IEnumerable<GestionCursoResponseDTO>>> ListarGestionCursos();

        Task<OperationResult> ActualizarGestionCurso(CrearGestionCursoDTO dto);
    }
}
