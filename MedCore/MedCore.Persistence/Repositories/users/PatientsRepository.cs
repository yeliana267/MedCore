
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.users
{
    public class PatientsRepository : BaseRepository<Patients, int>, IPatientsRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<PatientsRepository> _logger;
        private readonly IConfiguration _configuration;
        public PatientsRepository(MedCoreContext context, ILogger<PatientsRepository> loger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = loger;
            _configuration = configuration;
        }

        Task<OperationResult> IPatientsRepository.UpdatePatientProfileAsync(Patients patient)
        {
            throw new NotImplementedException();
        }
    }
}
