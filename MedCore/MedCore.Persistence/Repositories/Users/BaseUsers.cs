

using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Interfaces.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.Persistence.Repositories.Users
{
    public class BaseUsers : IBaseUsers
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MedCoreContext _context;
        private bool _disposed = false;

        public IUserRepository UserRepository => _serviceProvider.GetRequiredService<IUserRepository>();
        public IDoctorRepository DoctorRepository => _serviceProvider.GetRequiredService<IDoctorRepository>();
        public IPatientRepository PatientRepository => _serviceProvider.GetRequiredService<IPatientRepository>();



        public BaseUsers(MedCoreContext context, IServiceProvider serviceProvider)
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
