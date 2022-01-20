using JobProcessing.Api.Contracts;
using JobProcessing.Api.Data;
using Microsoft.Extensions.DependencyInjection;

namespace JobProcessing.Api.Infrastructure.Installers
{
    public class RegisterFakeData : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services)
        {
            services.AddSingleton<IFakeJobCollection, FakeJobCollection>();
        }
    }
}
