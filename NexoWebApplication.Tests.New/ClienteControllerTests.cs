using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoWebApplication.Controllers;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Infrastructure.Data;
using NexoWebApplication.Application.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace NexoWebApplication.Tests
{
        public class ClienteControllerTests
        {
            private class TestClienteController : ClienteController
            {
                public TestClienteController(Application.Interfaces.IClienteService service) : base(service) { }

                protected override object[] GetLinksForGetAll(int page, int pageSize)
                {
                    return new[]
                    {
                        new { rel = "self", href = "/fake/self" },
                        new { rel = "next", href = "/fake/next" },
                        new { rel = "prev", href = "/fake/prev" }
                    };
                }

                protected override object[] GetLinksForGetById(int id)
                {
                    return new[]
                    {
                        new { rel = "self", href = $"/fake/{id}" },
                        new { rel = "all", href = "/fake/all" }
                    };
                }
            }
        private AppDbContext SetupDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        private void SetupControllerForUrl(ControllerBase controller)
        {
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            controller.ControllerContext = new ControllerContext(actionContext);
            controller.Url = new UrlHelper(actionContext);
        }

        [Fact]
        public async Task GetById_ReturnsOkResultWithCliente()
        {
            var dbContext = SetupDbContext();
            var cliente = new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" };
            dbContext.Clientes.Add(cliente);
            dbContext.SaveChanges();
            var service = new ClienteService(new Infrastructure.Repositories.ClienteRepository(dbContext));
            var controller = new TestClienteController(service);

            var result = await controller.GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var response = okResult.Value;
            Assert.NotNull(response);
            var dataProp = response.GetType().GetProperty("data");
            Assert.NotNull(dataProp);
            var clienteResp = dataProp.GetValue(response) as Cliente;
            Assert.NotNull(clienteResp);
            Assert.Equal(1, clienteResp.Id);
            Assert.Equal("Teste", clienteResp.Nome);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenClienteDoesNotExist()
        {
            var dbContext = SetupDbContext();
            var service = new ClienteService(new Infrastructure.Repositories.ClienteRepository(dbContext));
            var controller = new TestClienteController(service);

            var result = await controller.GetById(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResultWithClientes()
        {
            var dbContext = SetupDbContext();
            dbContext.Clientes.AddRange(new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" },
                                        new Cliente { Id = 2, Nome = "Outro", Email = "outro@teste.com", Senha = "456" });
            dbContext.SaveChanges();
            var service = new ClienteService(new Infrastructure.Repositories.ClienteRepository(dbContext));
            var controller = new TestClienteController(service);

            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var response = okResult.Value;
            Assert.NotNull(response);
            var totalCountProp = response.GetType().GetProperty("totalCount");
            Assert.NotNull(totalCountProp);
            Assert.Equal(2, (int)totalCountProp.GetValue(response));
            var dataProp = response.GetType().GetProperty("data");
            Assert.NotNull(dataProp);
            var clientesList = dataProp.GetValue(response) as System.Collections.IEnumerable;
            Assert.NotNull(clientesList);
            var clientes = new List<Cliente>();
            foreach (var item in clientesList)
            {
                clientes.Add(item as Cliente);
            }
            Assert.Equal("Teste", clientes[0].Nome);
            Assert.Equal("Outro", clientes[1].Nome);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenClienteIsUpdated()
        {
            var dbContext = SetupDbContext();
            dbContext.Clientes.Add(new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" });
            dbContext.SaveChanges();
            var service = new ClienteService(new Infrastructure.Repositories.ClienteRepository(dbContext));
            var controller = new TestClienteController(service);

            var tracked = dbContext.Clientes.Local.FirstOrDefault(c => c.Id == 1);
            if (tracked != null)
                dbContext.Entry(tracked).State = EntityState.Detached;

            var updated = new Cliente { Id = 1, Nome = "Atualizado", Email = "atualizado@teste.com", Senha = "789" };
            var result = await controller.Update(1, updated);
            Assert.IsType<NoContentResult>(result);
            var clienteDb = await dbContext.Clientes.FindAsync(1);
            Assert.NotNull(clienteDb);
            Assert.Equal("Atualizado", clienteDb.Nome);
            Assert.Equal("atualizado@teste.com", clienteDb.Email);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenClienteIsDeleted()
        {
            var dbContext = SetupDbContext();
            dbContext.Clientes.Add(new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" });
            dbContext.SaveChanges();
            var service = new ClienteService(new Infrastructure.Repositories.ClienteRepository(dbContext));
            var controller = new TestClienteController(service);

            var result = await controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
            var clienteDb = await dbContext.Clientes.FindAsync(1);
            Assert.Null(clienteDb);
        }
    }
}



