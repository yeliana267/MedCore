
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MedCoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MedCoreDbContext"));
            });

            // dependencias medical
            builder.Services.AddScoped<IAvailabilityModesRepository , AvailabilityModesRepository>();
            builder.Services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
            builder.Services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();

            builder.Services.AddControllers();
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
