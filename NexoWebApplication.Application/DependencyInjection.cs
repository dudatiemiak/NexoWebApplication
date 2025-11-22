using Microsoft.Extensions.DependencyInjection;
using NexoWebApplication.Application.Interfaces;
using NexoWebApplication.Application.Services;

namespace NexoWebApplication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IDescricaoClienteService, DescricaoClienteService>();
            return services;
        }
    }
}