using Xunit;
using NexoWebApplication.Application.Services;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Domain.Repositories;
using Moq;

namespace NexoWebApplication.Tests
{
    public class ClienteServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsCliente()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" });
            var service = new ClienteService(mockRepo.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Teste", result.Nome);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsClientes()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(new List<Cliente> {
                new Cliente { Id = 1, Nome = "Teste1", Email = "t1@teste.com", Senha = "123" },
                new Cliente { Id = 2, Nome = "Teste2", Email = "t2@teste.com", Senha = "456" }
            });
            var service = new ClienteService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Cliente>)result).Count);
        }
    }
}
