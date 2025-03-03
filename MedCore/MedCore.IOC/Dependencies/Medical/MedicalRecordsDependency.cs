using MedCore.Application.Interfaces.Medical;
using MedCore.Application.Services.Medical;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Medical
{
    public class MedicalRecordsDependency
    {
        public static void AddMedicalRecordsDependency(IServiceCollection services)
        {
            services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
            services.AddScoped<IMedicalRecordsService, MedicalRecordsService>();
        }
    }
}
