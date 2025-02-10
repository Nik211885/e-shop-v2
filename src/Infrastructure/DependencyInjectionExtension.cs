using Application.Interface;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services.Memory;
using Infrastructure.Services.Notification;
using Infrastructure.Services.SlaveDbSelector;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddTransient<ISendNotification, SendNotificationByEmail>();
            services.AddDbContext<EfApplicationDbContext>((servicesProvider,options) =>
            {
                // have one master but have many slave
                var httpContextAccessor = servicesProvider.GetService<IHttpContextAccessor>();
                string requestMethod = httpContextAccessor?.HttpContext?.Request.Method ?? "POST";
                string connectionString;
                if (requestMethod == "GET")
                {
                    var slaveDbServices = servicesProvider.GetService<ISlaveDbSelector>() 
                        ?? throw new NullReferenceException("Slave not yet setup");

                    connectionString = slaveDbServices.GetConnectionString();
                }
                else
                {
                    var configuration = servicesProvider.GetService<IConfiguration>()
                        ?? throw new NullReferenceException("Configuration not yet dependency injection");
                    connectionString = configuration.GetSection("ConnectionStrings:Master").Value
                        ?? throw new NullReferenceException("Master not yet setup");
                }
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}