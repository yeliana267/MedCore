

using System.ComponentModel.Design;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Application.Services.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.Insurance;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.IOC.Dependencies.Insurance
{
    public static class NetworkTypeDependency
    {
        public static void AddNetworkTypeDependency(this IServiceCollection service)
        {
            service.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();
            service.AddTransient<INetworkTypeService, NetworkTypeService>();

        }
    }
}
 