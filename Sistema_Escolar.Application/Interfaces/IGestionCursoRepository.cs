using System;
using System.Collections.Generic;
using System.Text;
using Sistema_Escolar.Application.Common;
using Sistema_Escolar.Application.DTOs;

namespace Sistema_Escolar.Application.Interfaces
{
    public interface IGestionCursoRepository
    {
        Task<OperationResult> CreateGestionCurso(CrearGestionCursoDTO dto);

        Task<OperationResult<IEnumerable<GestionCursoResponseDTO>>> GetAllGestionCursos();

        Task<OperationResult<IEnumerable<GestionCursoBaseDTO>>> GetIdGestiomCurso(int id);

        Task<OperationResult> UpdateGestionCurso(CrearGestionCursoDTO dto);
    }
}
