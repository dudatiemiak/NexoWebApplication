using Xunit;
using NexoWebApplication.Application.Services;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Domain.Repositories;


namespace NexoWebApplication.Tests
{
    public class ClienteServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsCliente()
        {

            var fakeRepo = new FakeClienteRepository(new List<Cliente> {
                new Cliente { Id = 1, Nome = "Teste", Email = "teste@teste.com", Senha = "123" }
            });
            var service = new ClienteService(fakeRepo);


            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Teste", result.Nome);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsClientes()
        {

            var fakeRepo = new FakeClienteRepository(new List<Cliente> {
                new Cliente { Id = 1, Nome = "Teste1", Email = "t1@teste.com", Senha = "123" },
                new Cliente { Id = 2, Nome = "Teste2", Email = "t2@teste.com", Senha = "456" }
            });
            var service = new ClienteService(fakeRepo);


            var result = await service.GetAllAsync(1, 10);

            Assert.NotNull(result);
            Assert.Equal(2, ((List<Cliente>)result).Count);
    }

    public class FakeClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes;
        public FakeClienteRepository(List<Cliente>? clientes = null)
        {
            _clientes = clientes ?? new List<Cliente>();
        }
        public Task AddAsync(Cliente cliente)
        {
            _clientes.Add(cliente);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(int id)
        {
            _clientes.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Cliente>> GetAllAsync(int page, int pageSize)
        {
            return Task.FromResult<IEnumerable<Cliente>>(_clientes);
        }
        public Task<Cliente> GetByEmailAsync(string email)
        {
            return Task.FromResult(_clientes.Find(c => c.Email == email)!);
        }
        public Task<Cliente> GetByIdAsync(int id)
        {
            return Task.FromResult(_clientes.Find(c => c.Id == id)!);
        }
        public Task UpdateAsync(Cliente cliente)
        {
            var idx = _clientes.FindIndex(x => x.Id == cliente.Id);
            if (idx >= 0) _clientes[idx] = cliente;
            return Task.CompletedTask;
        }
    }

    }
}
