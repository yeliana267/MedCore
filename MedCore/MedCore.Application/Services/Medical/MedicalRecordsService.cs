using MedCore.Application.Dtos.Medical.MedicalRecordsDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Medical
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;
        private readonly ILogger<MedicalRecordsService> _logger;
        private readonly IConfiguration _configuration;

        public MedicalRecordsService(IMedicalRecordsRepository medicalRecordsRepository,
            ILogger<MedicalRecordsService> logger,
            IConfiguration configuration)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
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

        public Task<OperationResult> Remove(RemoveMedicalRecordsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveMedicalRecordsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateMedicalRecordsDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
