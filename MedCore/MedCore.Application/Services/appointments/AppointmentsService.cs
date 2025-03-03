using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.appointments
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
         private readonly IAppointmentsRepository _appointmentsRepository;
        public AppointmentsService(IAppointmentsRepository appointmentsRepository,
            ILogger<AppointmentsService> logger, 
            IConfiguration configuration) {

            _configuration = configuration;
            _logger = logger;
            _appointmentsRepository = appointmentsRepository;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = await _appointmentsRepository.GetAllAsync();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveAppointmentsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveAppointmentsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateAppointmentsDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
