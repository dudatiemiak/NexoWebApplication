using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Domain.Entities;

namespace NexoWebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var clientes = await _clienteService.GetAllAsync(page, pageSize);
            var totalCount = clientes.Count();
            var response = new
            {
                data = clientes,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetAll), new { page, pageSize }) },
                    new { rel = "next", href = Url.Action(nameof(GetAll), new { page = page + 1, pageSize }) },
                    new { rel = "prev", href = Url.Action(nameof(GetAll), new { page = page > 1 ? page - 1 : 1, pageSize }) }
                },
                totalCount
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            var response = new
            {
                data = cliente,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetById), new { id }) },
                    new { rel = "all", href = Url.Action(nameof(GetAll)) }
                }
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente model)
        {
            if (id != model.Id) return BadRequest();
            await _clienteService.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}