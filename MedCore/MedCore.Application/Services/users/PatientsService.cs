using MedCore.Application.Dtos.users.Patients;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace MedCore.Application.Services.users
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsService> _logger;
        private readonly IConfiguration _configuration;

        public PatientsService(IPatientsRepository patientsRepository, ILogger<PatientsService> logger, IConfiguration configuration)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemovePatientsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SavePatientsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdatePatientsDto dto)
        {
            throw new NotImplementedException();
        }
    }
    }
