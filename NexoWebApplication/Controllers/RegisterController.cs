using Microsoft.AspNetCore.Mvc;
using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Domain.Entities;

namespace NexoWebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public RegisterController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Cliente model)
        {
            await _clienteService.AddAsync(model);
            return CreatedAtAction(nameof(Register), new { id = model.Id }, model);
        }
    }
}