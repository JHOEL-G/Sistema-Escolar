using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloRecursoController : ControllerBase
    {
        private readonly IModulosRecursosService _service;

        public ModuloRecursoController(IModulosRecursosService service)
        {
            _service = service;
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarModulosRecursos([FromBody] AgregarModulosCursoDTO dto)
            => Ok(await _service.AgregarModulosRecursos(dto));
    }
}
