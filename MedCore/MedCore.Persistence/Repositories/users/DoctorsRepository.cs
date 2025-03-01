

using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.users
{
    public class DoctorsRepository : BaseRepository<Doctors, int>, IDoctorsRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<DoctorsRepository> _logger;
        private readonly IConfiguration _configuration;
        public DoctorsRepository(MedCoreContext context, ILogger<DoctorsRepository> loger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = loger;
            _configuration = configuration;
        }

        Task<OperationResult> IDoctorsRepository.UpdateDoctorProfileAsync(Doctors doctor)
        {
            throw new NotImplementedException();
        }
    }
  
}
