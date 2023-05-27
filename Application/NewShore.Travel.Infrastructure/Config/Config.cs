using Microsoft.Extensions.DependencyInjection;
using NewShore.Travel.Domain.Features.Flights.Contracts.Integrations;
using NewShore.Travel.Infrastructure.Integrations.Flights.NewShore;

namespace NewShore.Travel.Infrastructure.Config
{
    public static class Config
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFlightsService), typeof(NewShoreFlightService));
            return services;
        }
    }
}
