
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;

using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.appointments;
using MedCore.Persistence.Repositories.Insurance;


using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;

using Microsoft.EntityFrameworkCore;
using MedCore.Persistence.Interfaces.System;
using MedCore.Persistence.Repositories.System;

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

            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<INotificationsRepository, NotificationsRepository>();
             builder.Services.AddScoped<IStatusRepository, StatusRepository>();
             builder.Services.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();
            builder.Services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();
            builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            builder.Services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            builder.Services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
            builder.Services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
            builder.Services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();



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
