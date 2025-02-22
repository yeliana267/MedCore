

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace MedCore.Persistence.Repositories.appointments
{
    public class AppointmentsRepository : BaseRepository<Appointments, int>, IAppointmentsRepository
    {

        private readonly MedCoreContext _context;
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly IConfiguration _configuration;

        public AppointmentsRepository(MedCoreContext context, ILogger<AppointmentsRepository> logger, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.DoctorID == doctorId)
                    .ToListAsync();
                result.Data = appointments;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByDoctor"] ?? "Error desconocido al obtener citas del doctor.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);

            }
            return result;


        }
        public async Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.PatientID == patientId)
                    .ToListAsync();

                result.Data = appointments;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByPatient"] ?? "Error desconocido al obtener citas del paciente.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }
            return result;
        }



        public async Task<OperationResult> GetAppointmentsByDateAsync(DateTime date)
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.AppointmentDate.Date == date.Date)
                    .ToListAsync();

                result.Data = appointments;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByDate"] ?? "Error desconocido al obtener citas por fecha.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }

            return result;
        }
        public async Task<OperationResult> CancelAppointmentAsync(int appointmentId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointment = await _context.Appointments.FindAsync(appointmentId);
                if (appointment == null)
                {
                    result.Message = "Cita no encontrada.";
                    result.Success = false;
                    return result;
                }

                appointment.StatusID = 3;
                await _context.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:CancelAppointment"] ?? "Error desconocido al cancelar la cita.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }

            return result;
        }
        public async Task<OperationResult> ConfirmAppointmentAsync(int appointmentId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointment = await _context.Appointments.FindAsync(appointmentId);
                if (appointment == null)
                {
                    result.Message = "Cita no encontrada.";
                    result.Success = false;
                    return result;
                }

                appointment.StatusID = 2;
                await _context.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:ConfirmAppointment"] ?? "Error desconocido al confirmar la cita.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }

            return result;
        }
        public async Task<OperationResult> GetPendingAppointmentsAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.StatusID == 1) 
                    .ToListAsync();

                result.Data = appointments;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetPendingAppointments"] ?? "Error desconocido al obtener citas pendientes.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }

            return result;
        }
        public async Task<OperationResult> GetConfirmedAppointmentsAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.StatusID == 2) 
                    .ToListAsync();

                result.Data = appointments;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetConfirmedAppointments"] ?? "Error desconocido al obtener citas confirmadas.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }

            return result;
        }
        public async Task<OperationResult> GetCancelledAppointmentsAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.StatusID == 3)
                    .ToListAsync();

                result.Data = appointments;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetCancelledAppointments"] ?? "Error desconocido al obtener citas canceladas.";
                result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);
            }

            return result;
        }
        public override Task<OperationResult> SaveEntityAsync(Appointments entity)
        {
            return base.SaveEntityAsync(entity);
        }

        public override Task<OperationResult> UpdateEntityAsync(Appointments entity)
        {
            return base.UpdateEntityAsync(entity);
        }

    }

}
