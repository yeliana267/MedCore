

using MedCore.Application.Interfaces.appointments;
using MedCore.Application.Services.appointments;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Repositories.appointments;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.appointments
{
    public static class DoctorAvailabilityDependency
    {
        public static void AddDoctorAvailabilityDependency(this IServiceCollection service)
        {
            service.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            service.AddTransient<IDoctorAvailabilityService, DoctorAvailabilityService>();
        }
    }
}
