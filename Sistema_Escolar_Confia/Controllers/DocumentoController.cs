using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_Escolar.Application.DTOs;
using Sistema_Escolar.Application.Interfaces.IServices;

namespace Sistema_Escolar_Confia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IGestionDocumentoService _service;

        public DocumentoController(IGestionDocumentoService service)
        {
            _service = service;
        }

        [HttpGet("{exploradorId}")]
        public async Task<IActionResult> ListarDocumentosPorCarpeta(int exploradorId)
        {
            var resultado = await _service.ListarDocumentosPorCarpeta(exploradorId);

            if (!resultado.Success) return BadRequest(resultado);
            if (resultado.Data == null || !resultado.Data.Any()) return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearDocumento([FromForm] string documentoData, IFormFile? archivo)
        {
            try
            {
                var archivoDocumento = JsonSerializer.Deserialize<DocumentoInsertarDTO>(documentoData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                using Stream? archivoStream = archivo?.OpenReadStream();

                if (archivoDocumento == null) return BadRequest("Datos del documento inválidos");


                var resultado = await _service.CrearDocumento(archivoDocumento, archivoStream, archivo?.FileName);

                return Ok(resultado);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { success = false, message = $"Error al deserializar: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDocumento(int id, [FromBody] DocumentoActualizarDTO dto)
        {
            dto.GestionId = id;

            var resultado = await _service.ActualizarDocumento(dto);

            if (!resultado.Success) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDocumento(int id)
        {
            var resultado = await _service.EliminarDocumento(id);

            if (!resultado.Success) return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
