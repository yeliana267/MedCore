using MedCore.Application.Interfaces.appointments;
using MedCore.IOC.Dependencies.appointments;
using MedCore.Persistence.Context;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.appointments;
using MedCore.Web.Repositories;
using MedCore.Web.Repositories.appointments;
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

            // Configuración de HttpClient
            builder.Services.AddHttpClient();

            // Registro de servicios
            builder.Services.AddScoped<IApiClient, ApiClient>();
            builder.Services.AddAppointmentsDependency();

            builder.Services.AddScoped<IAppointmentWeb, AppointmentWeb>();

            // Configuración de DbContext
            builder.Services.AddDbContext<MedCoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));

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
