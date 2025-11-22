using Xunit;
using Moq;
using NexoWebApplication.Controllers;
using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexoWebApplication.Tests
{
    public class ClienteControllerTests
    {
        [Fact]
        public async Task GetById_ReturnsOkResultWithCliente()
        {
            // Arrange
            var mockService = new Mock<IClienteService>();
            mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" });
            var controller = new ClienteController(mockService.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal(1, (int)response.data.Id);
            Assert.Equal("Teste", (string)response.data.Nome);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenClienteDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IClienteService>();
            mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Cliente)null);
            var controller = new ClienteController(mockService.Object);

            // Act
            var result = await controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
