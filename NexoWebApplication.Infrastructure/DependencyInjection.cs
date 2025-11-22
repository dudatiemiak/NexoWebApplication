using Microsoft.Extensions.DependencyInjection;
using NexoWebApplication.Domain.Repositories;
using NexoWebApplication.Infrastructure.Repositories;

namespace NexoWebApplication.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IDescricaoClienteRepository, DescricaoClienteRepository>();
            return services;
        }
    }
}