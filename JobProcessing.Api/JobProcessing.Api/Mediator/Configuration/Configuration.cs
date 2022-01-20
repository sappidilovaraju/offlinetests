using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobProcessing.Api.Mediator.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
