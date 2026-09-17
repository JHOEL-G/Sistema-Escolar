using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionNotificaionesController : ControllerBase
    {
        private readonly IConfiguracionNotificacionesService _service;

        public ConfiguracionNotificaionesController(IConfiguracionNotificacionesService service)
        {
            _service = service;
        }

        [HttpGet("{categoria}")]
        public async Task<IActionResult> ListarConfiguracionByCategoria(string categoria)
        {
            var resultado = await _service.ListarConfiguracionByCategoria(categoria);

            if (!resultado.Success)
                return BadRequest(resultado); 

            return Ok(resultado);
        }

        [HttpPut("toggle")]
        public async Task<IActionResult> AlterarConfiguracion([FromBody] UpdateConfiguracionRequestDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos de configuración son requeridos.");

            var resultado = await _service.AlterarConfiguracion(dto);

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
