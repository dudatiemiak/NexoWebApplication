using Microsoft.EntityFrameworkCore;
using NexoWebApplication.Domain.Entities;
using NexoWebApplication.Domain.Repositories;
using NexoWebApplication.Infrastructure.Data;

namespace NexoWebApplication.Infrastructure.Repositories
{
    public class DescricaoClienteRepository : IDescricaoClienteRepository
    {
        private readonly AppDbContext _context;
        public DescricaoClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DescricaoCliente> GetByIdAsync(int id)
        {
            return await _context.DescricoesCliente.Include(d => d.Cliente).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DescricaoCliente>> GetAllAsync(int page, int pageSize)
        {
            return await _context.DescricoesCliente.Include(d => d.Cliente)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(DescricaoCliente descricao)
        {
            await _context.DescricoesCliente.AddAsync(descricao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DescricaoCliente descricao)
        {
            _context.DescricoesCliente.Update(descricao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var descricao = await _context.DescricoesCliente.FindAsync(id);
            if (descricao != null)
            {
                _context.DescricoesCliente.Remove(descricao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DescricaoCliente>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.DescricoesCliente.Where(d => d.ClienteId == clienteId).ToListAsync();
        }
    }
}