using NexoWebApplication.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexoWebApplication.Domain.Repositories
{
    public interface IDescricaoClienteRepository
    {
        Task<DescricaoCliente> GetByIdAsync(int id);
        Task<IEnumerable<DescricaoCliente>> GetAllAsync(int page, int pageSize);
        Task AddAsync(DescricaoCliente descricao);
        Task UpdateAsync(DescricaoCliente descricao);
        Task DeleteAsync(int id);
        Task<IEnumerable<DescricaoCliente>> GetByClienteIdAsync(int clienteId);
    }
}