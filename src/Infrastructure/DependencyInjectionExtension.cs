using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services.SlaveDbSelector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ISlaveDbSelector, SlaveDbSelector>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<EfApplicationDbContext>();
            return services;
        }
    }
}