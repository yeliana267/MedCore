
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

        public async Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var result = new OperationResult();

            try
            {
                // Suponiendo que Appointment es la entidad que representa una cita
                var appointments = await _context.Appointments
                                                  .Where(a => a.DoctorID == doctorId)
                                                  .ToListAsync();

                if (appointments.Any())
                {
                    result.Data = appointments; // Asignamos las citas encontradas a los datos del resultado
                    result.Success = true;
                    result.Message = "Citas obtenidas correctamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontraron citas para este doctor.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId)
        {
            var result = new OperationResult();
            try
            {
             
                var appointments = await _context.Appointments
                                                  .Where(a => a.PatientID == patientId)
                                                  .ToListAsync();
                if (appointments.Any())
                {
                    result.Data = appointments; 
                    result.Success = true;
                    result.Message = "Citas obtenidas correctamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontraron citas para este paciente.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error: {ex.Message}";
            }
            return result;
        }
        public async Task<OperationResult> GetPendingAppointmentsAsync()
        {
            var result = new OperationResult();
            try
            {
                var appointments = await _context.Appointments
                                                  .Where(a => a.StatusID == 1)
                                                  .ToListAsync();
                if (appointments.Any())
                {
                    result.Data = appointments;
                    result.Success = true;
                    result.Message = "Citas pendientes obtenidas correctamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontraron citas pendientes.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error: {ex.Message}";
            }
            return result;
        }
    }
}
