using MedCore.Application.Interfaces.Medical;
using MedCore.Application.Services.Medical;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Medical
{
    public static class MedicalRecordsDependency
    {
        public static void AddMedicalRecordsDependency(this IServiceCollection services)
        {
            services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
            services.AddTransient<IMedicalRecordsService, MedicalRecordsService>();
        }
    }
}
