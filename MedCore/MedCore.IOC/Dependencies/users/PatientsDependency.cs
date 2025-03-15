

using MedCore.Application.Interfaces.users;
using MedCore.Application.Services.users;
using MedCore.Persistence.Interfaces.users;
using MedCore.Persistence.Repositories.users;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.users
{
   public static class PatientsDependency
    {
        public static void AddPatientsDependency(this IServiceCollection services)
        {
            services.AddScoped<IPatientsRepository, PatientsRepository>();
            services.AddScoped<IPatientsService, PatientsService>();
        }
    }
}
