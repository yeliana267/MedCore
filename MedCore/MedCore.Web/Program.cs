using MedCore.IOC.Dependencies.Medical;
using MedCore.Persistence.Context;
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
