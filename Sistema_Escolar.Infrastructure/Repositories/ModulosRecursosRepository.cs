using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Infrastructure.Data;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class ModulosRecursosRepository : IModulosRecursosRepository
    {
        private readonly ConfiaContext _context;

        public ModulosRecursosRepository(ConfiaContext context)
        {
            _context = context;
        }

        public async Task<object> AgregarModulosRecursos(AgregarModulosCursoDTO dto)
        {
            var modulosTable = new DataTable();
            modulosTable.Columns.Add("Orden", typeof(int));
            modulosTable.Columns.Add("ModuloTitulo", typeof(string));
            modulosTable.Columns.Add("Descripcion", typeof(string));

            foreach (var m in dto.Modulos)
                modulosTable.Rows.Add(m.Orden, m.ModuloTitulo, m.Descripcion);

            var recursosTable = new DataTable();
            recursosTable.Columns.Add("ModuloIndice", typeof(int));
            recursosTable.Columns.Add("RecursoId", typeof(int));
            recursosTable.Columns.Add("OrdenRecurso", typeof(int));
            recursosTable.Columns.Add("Titulo", typeof(string));
            recursosTable.Columns.Add("Descripcion", typeof(string));

            foreach (var r in dto.Recursos)
                recursosTable.Rows.Add(r.ModuloId, r.RecursoId, r.OrdenRecurso, r.Titulo, r.Descripcion);

            var connection = _context.Database.GetDbConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@CursoId", dto.CursoId);
            parametros.Add("@Modulos", modulosTable.AsTableValuedParameter("dbo.TipoModulo"));
            parametros.Add("@Recursos", recursosTable.AsTableValuedParameter("dbo.TipoRecurso"));

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync(
                "dbo.sp_AgregarModulosRecursos",
                parametros,
                commandType: CommandType.StoredProcedure
            ) ?? new { CursoId = dto.CursoId, Success = 0, Mensaje = "Sin respuesta" };
        }
    }
}
