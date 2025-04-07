using MedCore.Application.Interfaces.appointments;
using MedCore.IOC.Dependencies.appointments;

using MedCore.Persistence.Context;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.appointments.appointment;
using MedCore.Web.Interfaces.appointments.doctorAvailability;
using MedCore.Web.Repositories;
using MedCore.Web.Repositories.appointmentsRepository.appointment;
using MedCore.Web.Repositories.appointmentsRepository.doctorAvailability;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;



namespace MedCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            // Configuraci�n de HttpClient
            builder.Services.AddHttpClient();

            // Registro de servicios
            builder.Services.AddScoped<IApiClient, ApiClient>();
            builder.Services.AddAppointmentsDependency();

            builder.Services.AddDbContext<MedCoreContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));
            
            builder.Services.AddUsersDependency();
            builder.Services.AddPatientsDependency();
            builder.Services.AddDoctorsDependency();
            builder.Services.AddControllers();
       


            builder.Services.AddScoped<IAppointmentWeb, AppointmentWeb>();
            builder.Services.AddScoped<IDoctorAvailabilityWeb, DoctorAvailabilityWeb>();


            // Configuraci�n de DbContext
            builder.Services.AddDbContext<MedCoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MedCoreContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));
            builder.Services.AddAvailabilityModesDependency();
            builder.Services.AddMedicalRecordsDependency();
            builder.Services.AddSpecialtiesDependency();

            // Otros servicios
            builder.Services.AddAppointmentsDependency();
            builder.Services.AddDoctorAvailabilityDependency();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
