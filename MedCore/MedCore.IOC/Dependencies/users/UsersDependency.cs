

using System.ComponentModel.Design;
using MedCore.Application.Interfaces.users;
using MedCore.Application.Services.users;
using MedCore.Persistence.Interfaces.users;
using MedCore.Persistence.Repositories.users;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.users
{
    public static class UsersDependency
    {
        public static void AddUsersDependency(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();

        }
    }
}
