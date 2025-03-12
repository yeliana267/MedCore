

using MedCore.Application.Interfaces.Insurance;
using MedCore.Application.Services.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.Insurance;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Insurance
{
    public static class InsuranceProvidersDependency
    {
        public static void AddInsuranceProvidersDependency(this IServiceCollection service)
        {
            service.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();
            service.AddTransient<IInsuranceProvidersService, InsuranceProvidersService>();
        }
    }
}
