

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.Users
{
    public class DoctorRepository : BaseRepository<Patient, int>, IDoctorRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;

        public DoctorRepository(MedCoreContext context,
                              ILogger<UserRepository> logger,
                              IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Doctor>> GetDoctorsBySpecialtyAsync(short specialtyId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> UpdateDoctorInfoAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> UpdateDoctorProfileAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
