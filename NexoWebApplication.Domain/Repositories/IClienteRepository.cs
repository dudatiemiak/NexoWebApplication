using NexoWebApplication.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexoWebApplication.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllAsync(int page, int pageSize);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
        Task<Cliente> GetByEmailAsync(string email);
    }
}