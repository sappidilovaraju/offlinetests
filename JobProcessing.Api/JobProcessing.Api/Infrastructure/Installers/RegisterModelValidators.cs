using FluentValidation;
using JobProcessing.Api.Contracts;
using JobProcessing.Api.DTO.Request;
using Microsoft.Extensions.DependencyInjection;

namespace JobProcessing.Api.Infrastructure.Installers
{
    public class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateJobRequest>, CreateJobRequestValidator>();
        }
    }
}
