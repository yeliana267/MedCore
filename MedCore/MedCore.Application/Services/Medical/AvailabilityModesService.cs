using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Medical
{
    public class AvailabilityModesService : IAvailabilityModesService
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository;
        private readonly ILogger<AvailabilityModesService> _logger;
        private readonly IConfiguration _configuration;

        public AvailabilityModesService(IAvailabilityModesRepository availabilityModesRepository,
            ILogger<AvailabilityModesService> logger,
            IConfiguration configuration)
        {
            _availabilityModesRepository = availabilityModesRepository;
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

        public Task<OperationResult> Remove(RemoveAvailabilityModesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveAvailibilityModesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateAvailibilityModesDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
