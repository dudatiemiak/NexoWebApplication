using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Domain.Repositories;

namespace NexoWebApplication.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }
        public async Task<Cliente> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task<IEnumerable<Cliente>> GetAllAsync(int page, int pageSize) => await _repository.GetAllAsync(page, pageSize);
        public async Task AddAsync(Cliente cliente) => await _repository.AddAsync(cliente);
        public async Task UpdateAsync(Cliente cliente) => await _repository.UpdateAsync(cliente);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
        public async Task<Cliente> GetByEmailAsync(string email) => await _repository.GetByEmailAsync(email);
    }
}