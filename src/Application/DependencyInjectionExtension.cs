using Application.Behavior;
using Application.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Application
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddlewareHandling>();
            services.AddMediatR(cfg =>
            {
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}