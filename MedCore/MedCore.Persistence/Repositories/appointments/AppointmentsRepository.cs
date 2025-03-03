
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.system;
using MedCore.Model.Models.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.appointments
{
    public class AppointmentsRepository : BaseRepository<Appointments, int>, IAppointmentsRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly IConfiguration _configuration;

        public AppointmentsRepository(MedCoreContext context, ILogger<AppointmentsRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            throw new NotImplementedException();

        }

        public async Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetPendingAppointmentsAsync()
        {
            throw new NotImplementedException();
        }







        public override Task<OperationResult> SaveEntityAsync(Appointments entity)
        {
            _logger.LogInformation($"Guardando nueva cita para el paciente {entity.PatientID}");
            return base.SaveEntityAsync(entity);
        }

    
    }
}
