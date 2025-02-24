
using MedCore.Domain.Repository;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

           builder.Services.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();


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
