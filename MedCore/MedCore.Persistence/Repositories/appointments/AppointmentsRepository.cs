
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.system;
using MedCore.Model.Models.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
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
        public override async Task<OperationResult> UpdateEntityAsync(int id, Appointments entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointment = await _context.Appointments.FindAsync(id);

                if (appointment == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró la cita con ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: Cita {id} no encontrada.");
                    return result;
                }

                _logger.LogInformation($"Actualizando cita {id} para el paciente {entity.PatientID}");

                appointment.PatientID = entity.PatientID;
                appointment.DoctorID = entity.DoctorID;
                appointment.AppointmentDate = entity.AppointmentDate;
                appointment.StatusID = entity.StatusID;
                appointment.UpdatedAt = DateTime.Now;

                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = $"Cita con ID {id} actualizada correctamente.";
                _logger.LogInformation($"Cita {id} actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar la cita: {ex.Message}";
                _logger.LogError($"Error en UpdateEntityAsync para la cita {id}: {ex.Message}", ex);
            }

            return result;
        }


    }
}
