

using MedCore.Application.Interfaces.appointments;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Repositories.appointments;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.appointments
{
    public static class AppointmentsDependency
    {
        public static void AddAppointmentsDependency(this IServiceCollection service)
        {
            service.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            service.AddTransient<IAppointmentsService, AppointmentsService>();
        }
    }
}
