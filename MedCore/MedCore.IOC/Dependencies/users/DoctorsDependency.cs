

using MedCore.Application.Interfaces.users;
using MedCore.Application.Services.users;
using MedCore.Persistence.Interfaces.users;
using MedCore.Persistence.Repositories.users;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.users
{
    public static class DoctorsDependency
    {
        public static void AddDoctorsDependency(this IServiceCollection services)
        {
            services.AddScoped<IDoctorsRepository, DoctorsRepository>();
            services.AddScoped<IDoctorsService, DoctorsService>();
        }
    }
}
