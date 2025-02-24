

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.Users
{
    public class PatientRepository : BaseRepository<Patient, int>, IPatientRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<PatientRepository> _logger;
        private readonly IConfiguration _configuration;

        public PatientRepository(MedCoreContext context,
                              ILogger<PatientRepository> logger,
                              IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
   
        public async Task<OperationResult> UpdateMedicalInfoAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> UpdatePatientProfileAsync(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
