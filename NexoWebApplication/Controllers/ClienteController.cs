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
                links = GetLinksForGetAll(page, pageSize),
                totalCount
            };
            return Ok(response);
        }

        protected virtual object[] GetLinksForGetAll(int page, int pageSize)
        {
            return new[]
            {
                new { rel = "self", href = Url?.Action(nameof(GetAll), new { page, pageSize }) ?? string.Empty },
                new { rel = "next", href = Url?.Action(nameof(GetAll), new { page = page + 1, pageSize }) ?? string.Empty },
                new { rel = "prev", href = Url?.Action(nameof(GetAll), new { page = page > 1 ? page - 1 : 1, pageSize }) ?? string.Empty }
            };
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
                links = GetLinksForGetById(id)
            };
            return Ok(response);
        }

        protected virtual object[] GetLinksForGetById(int id)
        {
            return new[]
            {
                new { rel = "self", href = Url?.Action(nameof(GetById), new { id }) ?? string.Empty },
                new { rel = "all", href = Url?.Action(nameof(GetAll)) ?? string.Empty }
            };
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