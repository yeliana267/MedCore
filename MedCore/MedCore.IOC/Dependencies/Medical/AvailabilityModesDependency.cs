using MedCore.Application.Interfaces.Medical;
using MedCore.Application.Services.Medical;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Medical
{
    public static class AvailabilityModesDependency
    {
        public static void AddAvailabilityModesDependency(this IServiceCollection services)
        {
            services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
            services.AddTransient<IAvailabilityModesService, AvailabilityModesService>();
        }
    }
}
