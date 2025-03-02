

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.system;
using MedCore.Domain.Entities.Users;
using MedCore.Model.Models.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            OperationResult result = new OperationResult();
            try
            {
                var querys = await (from Appointments in _context.Appointments
                                    join Doctor in _context.Doctors on Appointments.DoctorID equals Doctor.UserID
                                    join User in _context.Users on Doctor.Id equals User.UserID
                                    join Status in _context.Status on Appointments.StatusID equals Status.Id
                                    where Appointments.DoctorID == doctorId
                                    orderby Appointments.AppointmentDate descending
                                    select new AppointmentsModel()
                                    {
                                        AppointmentID = Appointments.Id,
                                        DoctorID = Appointments.DoctorID,
                                        FirstName = User.FirstName,
                                        LastName = User.LastName,
                                        StatusID = Appointments.StatusID,
                                        AppointmentDate = Appointments.AppointmentDate
                                    }).ToListAsync();

                result.Data = querys;
                result.Success = true;
                result.Message = "Citas obtenidas exitosamente.";

                _logger.LogInformation($"Se obtuvieron {querys.Count} citas para el DoctorID {doctorId}");
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByDoctor"]
                                 ?? "Error desconocido al obtener citas del doctor.";
                result.Success = false;
                _logger.LogError($"Error al obtener citas para el DoctorID {doctorId}", ex);
            }
            return result;
        }

        public async Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var querys = await (from Appointments in _context.Appointments
                                    where Appointments.PatientID == patientId
                                    orderby Appointments.AppointmentDate descending
                                    select new AppointmentsModel()
                                    {
                                        AppointmentID = Appointments.Id,
                                        PatientID = Appointments.PatientID,
                                        AppointmentDate = Appointments.AppointmentDate,
                                        CreatedAt = Appointments.CreatedAt,
                                    }).ToListAsync();

                result.Data = querys;
                result.Success = true;
                result.Message = "Citas obtenidas exitosamente.";

                _logger.LogInformation($"Se obtuvieron {querys.Count} citas para el PatientID {patientId}");
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByPatient"]
                                 ?? "Error desconocido al obtener citas de un paciente.";
                result.Success = false;
                _logger.LogError($"Error al obtener citas para el PatientID {patientId}", ex);
            }
            return result;
        }

        public async Task<OperationResult> GetPendingAppointmentsAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                bool pendingAppointmentsExist = await _context.Appointments.AnyAsync(a => a.StatusID == 1);
                if (!pendingAppointmentsExist)
                {
                    result.Success = false;
                    result.Message = "No hay citas pendientes registradas.";
                    return result;
                }

                var querys = await(from Appointments in _context.Appointments
                                  join Patient in _context.Patients on Appointments.PatientID equals Patient.UserID
                                  join User in _context.Users on Patient.Id equals User.UserID
                                  join Doctor in _context.Doctors on Appointments.DoctorID equals Doctor.UserID
                                  join DoctorUser in _context.Users on Doctor.Id equals DoctorUser.UserID
                                  join Status in _context.Status on Appointments.StatusID equals Status.Id
                                  where Appointments.StatusID == 1
                                  orderby Appointments.AppointmentDate ascending
                                  select new AppointmentsModel()
                                  {
                                      AppointmentID = Appointments.Id,
                                      PatientID = Appointments.PatientID,
                                      DoctorID = Appointments.DoctorID,
                                      FirstName = User.FirstName,
                                      LastName = User.LastName,
                                      StatusID = Appointments.StatusID,
                                      AppointmentDate = Appointments.AppointmentDate,
                                    
                                  }).ToListAsync();

                if (querys.Count == 0)
                {
                    result.Success = false;
                    result.Message = "No hay citas pendientes disponibles.";
                    return result;
                }
                result.Data = querys;
                result.Success = true;
                result.Message = "Citas pendientes obtenidas exitosamente.";
                _logger.LogInformation($"Se obtuvieron {querys.Count} citas pendientes.");
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetPendingAppointments"]
                      ?? "Error desconocido al obtener citas pendientes.";
                result.Success = false;
                _logger.LogError($"Error al obtener citas pendientes: {ex.Message}", ex);

            }
            return result;

        }







        public override Task<OperationResult> SaveEntityAsync(Appointments entity)
        {
            _logger.LogInformation($"Guardando nueva cita para el paciente {entity.PatientID}");
            return base.SaveEntityAsync(entity);
        }

    
    }
}
