using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Domain.Repositories;

namespace NexoWebApplication.Application.Services
{
    public class DescricaoClienteService : IDescricaoClienteService
    {
        private readonly IDescricaoClienteRepository _repository;
        public DescricaoClienteService(IDescricaoClienteRepository repository)
        {
            _repository = repository;
        }
        public async Task<DescricaoCliente> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task<IEnumerable<DescricaoCliente>> GetAllAsync(int page, int pageSize) => await _repository.GetAllAsync(page, pageSize);
        public async Task AddAsync(DescricaoCliente descricao) => await _repository.AddAsync(descricao);
        public async Task UpdateAsync(DescricaoCliente descricao) => await _repository.UpdateAsync(descricao);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
        public async Task<IEnumerable<DescricaoCliente>> GetByClienteIdAsync(int clienteId) => await _repository.GetByClienteIdAsync(clienteId);
    }
}