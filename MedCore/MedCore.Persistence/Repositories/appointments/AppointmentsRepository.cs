

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.system;
using MedCore.Domain.Entities.Users;
using MedCore.Model.Models;
using MedCore.Model.Models.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;
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
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var querys = await (from Appointments in _context.Appointments
                                    join Doctor in _context.Doctors on Appointments.DoctorID equals Doctor.IdDoctorID
                                    join User in _context.Users on Doctor.Id equals User.UserID
                                    join Status in _context.Status on Appointments.StatusID equals Status.Id
                                    where Appointments.DoctorID == doctorId orderby
                                     Appointments.AppointmentDate descending select new AppointmentsModel() 
                                     { 
                                      
                                         AppointmentID = Appointments.Id,
                                         DoctorID = Appointments.DoctorID,
                                         FirstName = User.FirstName,
                                         LastName= User.LastName,
                                         StatusID = Appointments.StatusID,
                                         AppointmentDate = Appointments.AppointmentDate
                    }).ToListAsync();

                result.Data = querys;
                result.Success = true;
                result.Message = "Citas obtenidas exitosamente.";

            }   
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByDoctor"]
                                 ?? "Error desconocido al obtener citas del doctor."; result.Success = false;
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", result.Message, ex);

            }
            return result;


        }
        public async Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var querys = await( from Appointments in _context.Appointments where Appointments.PatientID == patientId orderby
                                   Appointments.AppointmentDate descending
                                   select new AppointmentsModel()
                    {
                                  
                                    AppointmentID = Appointments.Id,
                                    PatientID = Appointments.PatientID,
                                    AppointmentDate = Appointments.AppointmentDate,
                                    CreatedAt= Appointments.CreatedAt,  

                                   }).ToListAsync();
                result.Data = querys;
                result.Success = true;
                result.Message = "Citas obtenidas exitosamente.";

            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAppointmentsRepository:GetAppointmentsByPatient"]
                                                 ?? "Error desconocido al obtener  citas de un paciente."; result.Success = false;
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
