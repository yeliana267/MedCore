using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Medical
{
    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        private readonly ILogger<SpecialtiesService> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesService(ISpecialtiesRepository specialtiesRepository,
            ILogger<SpecialtiesService> logger,
            IConfiguration configuration)
        {
            _specialtiesRepository = specialtiesRepository;
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

        public Task<OperationResult> Remove(RemoveSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
