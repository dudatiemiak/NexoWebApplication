using Microsoft.EntityFrameworkCore;
using NexoWebApplication.Domain.Entities;

namespace NexoWebApplication.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DescricaoCliente> DescricoesCliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("TN_CLIENTE");
            modelBuilder.Entity<DescricaoCliente>().ToTable("TN_DESCRICAO_CLIENTE");

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Descricoes)
                .WithOne(d => d.Cliente)
                .HasForeignKey(d => d.ClienteId);
        }
    }
}