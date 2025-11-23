using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Domain.Entities;

namespace NexoWebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DescricaoClienteController : ControllerBase
    {
        private readonly IDescricaoClienteService _descricaoService;
        public DescricaoClienteController(IDescricaoClienteService descricaoService)
        {
            _descricaoService = descricaoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var descricoes = await _descricaoService.GetAllAsync(page, pageSize);
            var totalCount = descricoes.Count();
            var response = new
            {
                data = descricoes,
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
            var descricao = await _descricaoService.GetByIdAsync(id);
            if (descricao == null) return NotFound();
            var response = new
            {
                data = descricao,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetById), new { id }) },
                    new { rel = "all", href = Url.Action(nameof(GetAll)) }
                }
            };
            return Ok(response);
        }

        [HttpGet("cliente/{clienteId}")]
        [Authorize]
        public async Task<IActionResult> GetByClienteId(int clienteId)
        {
            var descricoes = await _descricaoService.GetByClienteIdAsync(clienteId);
            return Ok(descricoes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] DescricaoCliente model)
        {
            // Ignora o objeto Cliente recebido e vincula apenas pelo ClienteId
            model.Cliente = null;
            await _descricaoService.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] DescricaoCliente model)
        {
            if (id != model.Id) return BadRequest();
            model.Cliente = null;
            await _descricaoService.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _descricaoService.DeleteAsync(id);
            return NoContent();
        }
    }
}