using JobProcessing.Api.Contracts;
using JobProcessing.Data;
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
