using MedCore.IOC.Dependencies.Medical;
using MedCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MedCore.IOC.Dependencies.users;
using MedCore.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MedCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MedCoreContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));
            
            builder.Services.AddUsersDependency();
            builder.Services.AddPatientsDependency();
            builder.Services.AddDoctorsDependency();
            builder.Services.AddControllers();

       
    


            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MedCoreContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));
            builder.Services.AddAvailabilityModesDependency();
            builder.Services.AddMedicalRecordsDependency();
            builder.Services.AddSpecialtiesDependency();


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
