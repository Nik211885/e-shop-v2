using Application.Interface;
using Core.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services.Cloud.UploadFile;
using Infrastructure.Services.Memory;
using Infrastructure.Services.Notification;
using Infrastructure.Services.Payment;
using Infrastructure.Services.SlaveDbSelector;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            var cloudinaryConfig = configuration.GetSection("Cloud:Cloudinary").Get<CloudinaryConfig>() 
                ?? throw new ArgumentNullException(nameof(CloudinaryConfig));
            var identityAuthConfig = configuration.GetSection("IdentityAuthentication").Get<IdentityAuthentication>()
                                     ?? throw new ArgumentNullException(nameof(IdentityAuthentication));
            var mailSetting = configuration.GetSection("MailSetting").Get<MailSetting>()
                              ?? throw new ArgumentNullException(nameof(MailSetting));
            services.AddSingleton(cloudinaryConfig);
            services.AddSingleton(identityAuthConfig);
            services.AddSingleton(mailSetting);
            // van de cac cau hinh nay nen de singleton ;)) neu muon cap nhat hay tat ung dung
            
            services.AddSingleton<ISlaveDbSelector, SlaveDbSelector>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddTransient<ISendNotification, SendNotificationByEmail>();
            services.AddScoped<IFactoryMethod<IPayment>, PaymentFactory>();
            services.AddScoped<IUploadFileBySignature, UploadFileBySignature>();
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