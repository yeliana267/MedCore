using MedCore.Application.Interfaces.Medical;
using MedCore.Application.Services.Medical;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Medical
{
    public class AvailabilityModesDependency
    {
        public static void AddAvailabilityModesDependency(IServiceCollection services)
        {
            services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
            services.AddScoped<IAvailabilityModesService, AvailabilityModesService>();
        }
    }
}
