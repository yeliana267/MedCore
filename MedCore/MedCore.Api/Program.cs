
using MedCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MedCore.IOC.Dependencies.appointments;
using MedCore.IOC.Dependencies.Insurance;
using MedCore.IOC.Dependencies.Medical;
using MedCore.IOC.Dependencies.users;
using MedCore.Application.Interfaces.users;
using MedCore.Application.Services.users;


namespace MedCore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.AddDbContext<MedCoreContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedcoreDb")));
            builder.Services.AddControllers();

            builder.Services.AddUsersDependency();
            builder.Services.AddPatientsDependency();
            builder.Services.AddDoctorsDependency();
            builder.Services.AddDoctorAvailabilityDependency();
            builder.Services.AddAppointmentsDependency();
            builder.Services.AddInsuranceProvidersDependency();
            builder.Services.AddNetworkTypeDependency();
            builder.Services.AddAvailabilityModesDependency();
            builder.Services.AddMedicalRecordsDependency();
            builder.Services.AddSpecialtiesDependency();

        


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
