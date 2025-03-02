

using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class BaseInsuranceRepository : IBaseInsuranceRepository
    {
        private readonly MedCoreContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed = false;

        public IInsuranceProvidersRepository InsuranceProvidersRepository => _serviceProvider.GetRequiredService<IInsuranceProvidersRepository>();
        public INetworkTypeRepository NetworkTypeRepository => _serviceProvider.GetRequiredService<INetworkTypeRepository>();
        public BaseInsuranceRepository(MedCoreContext context, IServiceProvider serviceProvider
    )
        {
            _context = context;
            _serviceProvider = serviceProvider;

        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }
    }
}
