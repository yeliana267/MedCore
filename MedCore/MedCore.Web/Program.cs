using MedCore.IOC.Dependencies.appointments;
using MedCore.IOC.Dependencies.Medical;
using MedCore.IOC.Dependencies.users;
using MedCore.Persistence.Context;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.appointments;
using MedCore.Web.Interfaces.Medical;
using MedCore.Web.Repositories;
using MedCore.Web.Repositories.appointments;
using MedCore.Web.Repositories.Medical;
using Microsoft.EntityFrameworkCore;



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
            builder.Services.AddAvailabilityModesDependency();
            builder.Services.AddMedicalRecordsDependency();
            builder.Services.AddSpecialtiesDependency();


            builder.Services.AddScoped<IAvailabilityModesWeb, AvailabilityModesWeb>();
            builder.Services.AddScoped<IMedicalRecordsWeb, MedicalRecordsWeb>();
            builder.Services.AddScoped<ISpecialtiesWeb, SpecialtiesWeb>();
            builder.Services.AddScoped<IAppointmentWeb, AppointmentWeb>();
            builder.Services.AddControllersWithViews();
          
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
