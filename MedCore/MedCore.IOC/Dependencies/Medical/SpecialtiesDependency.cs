using MedCore.Application.Interfaces.Medical;
using MedCore.Application.Services.Medical;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Medical
{
    public static class SpecialtiesDependency
    {
        public static void AddSpecialtiesDependency(this IServiceCollection services)
        {
            services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
            services.AddTransient<ISpecialtiesService, SpecialtiesService>();
        }
    }
}
