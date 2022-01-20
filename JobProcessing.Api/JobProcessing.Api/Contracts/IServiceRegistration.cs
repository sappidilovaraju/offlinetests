using Microsoft.Extensions.DependencyInjection;

namespace JobProcessing.Api.Contracts
{
    interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services);
    }
}
