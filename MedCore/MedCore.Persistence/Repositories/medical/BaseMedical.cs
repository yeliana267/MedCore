using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.DependencyInjection;

namespace MedCore.Persistence.Repositories.medical
{
    public class BaseMedical : IBaseMedical 
    {
        private readonly MedCoreContext _context;
        private readonly IServiceProvider _serviceprovider;
        private bool _disposable = false;

        public IAvailabilityModesRepository AvailabilityModesRepository => _serviceprovider.GetRequiredService<IAvailabilityModesRepository>();
        public IMedicalRecordsRepository MedicalRecordsRepository => _serviceprovider.GetRequiredService<IMedicalRecordsRepository>();
        public ISpecialtiesRepository SpecialtiesRepository => _serviceprovider.GetRequiredService<ISpecialtiesRepository>();

        public BaseMedical(MedCoreContext context, IServiceProvider serviceprovider)
        {
            _context = context;
            _serviceprovider = serviceprovider;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!_disposable)
            {
                _context.Dispose();
                _disposable = true;
            }

        }
    }
}
