using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexoWebApplication.Services;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Dtos;
using System.Security.Claims;

namespace NexoWebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IClienteService _clienteService;

        public AuthController(TokenService tokenService, IClienteService clienteService)
        {
            _tokenService = tokenService;
            _clienteService = clienteService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var cliente = await _clienteService.GetByEmailAsync(model.Email);
            if (cliente != null && cliente.Senha == model.Senha)
            {
                // Gera token JWT com claims relevantes
                var token = _tokenService.GenerateToken(cliente.Id, cliente.Email);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        [HttpGet("dados-protegidos")]
        [Authorize]
        public IActionResult GetDadosProtegidos()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok($"Olá {username}! Você pode acessar os dados protegidos do sistema.");
        }
    }
}