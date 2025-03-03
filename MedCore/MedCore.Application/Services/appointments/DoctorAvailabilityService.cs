
using MedCore.Application.Dtos.appointments.DoctorAvailability;
using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.appointments
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository,
            ILogger<DoctorAvailabilityService> logger,
            IConfiguration configuration)
        {

            _configuration = configuration;
            _logger = logger;
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
        }
        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveDoctorAvailabilityDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveDoctorAvailabilityDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateDoctorAvailabilityDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
