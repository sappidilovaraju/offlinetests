using JobProcessing.Api.Contracts;
using JobProcessing.Api.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace JobProcessing.Api.Infrastructure.Installers
{
    public class RegisterWorkerServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services)
        {
            // services.AddHostedService<JobProcessingWorker>();
        }
    }
}
