using JobProcessing.Api.Contracts;
using JobProcessing.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JobProcessing.Api.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services)
        {
            services.AddTransient<IJobService, JobService>();
        }
    }
}
