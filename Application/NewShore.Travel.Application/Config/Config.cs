using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NewShore.Travel.Application.Behaviours;
using System.Reflection;

namespace NewShore.Travel.Application.Config
{
    public static class Config
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorLoggerBehaviour<,>));
            return services;
        }
    }
}
