using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedCore.Persistence.Repositories.appointments
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability, int>, IDoctorAvailabilityRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<DoctorAvailabilityRepository> _logger;
        private readonly IConfiguration _configuration;
        public DoctorAvailabilityRepository(MedCoreContext context, ILogger<DoctorAvailabilityRepository> logger, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<OperationResult> GetAvailabilityByDoctorIdAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            OperationResult result = new OperationResult();

            try
            {
                var availabilities = await _context.DoctorAvailabilities
                    .Where(a => a.DoctorID == doctorId && a.AvailableDate >= startDate && a.AvailableDate <= endDate)
                    .ToListAsync();

                result.Data = availabilities;

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository:GetAvailabilityByDoctorId"] ?? "Error desconocido al obtener disponibilidad del doctor." ?? "Error desconocido al obtener citas del doctor.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }
        public async Task<OperationResult> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate, TimeOnly startTime, TimeOnly endTime)
        {
            OperationResult result = new OperationResult();

            try
            {
                var availabilities = await _context.DoctorAvailabilities
                                    .Where(a => a.DoctorID == doctorId && a.AvailableDate == appointmentDate && a.StartTime <= startTime && a.EndTime >= endTime)
                    .ToListAsync();

                result.Data = availabilities;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository:IsDoctorAvailable"] ?? "Error desconocido al verificar disponibilidad del doctor.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }
        public async Task<OperationResult> GetActiveAvailabilityByDoctorIdAsync(int doctorId)
        {

            OperationResult result = new OperationResult();
            try
            {
                var availabilities = await _context.DoctorAvailabilities
                    .Where(a => a.DoctorID == doctorId && a.AvailableDate >= DateTime.Now)
                    .ToListAsync();
                result.Data = availabilities;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository:GetActiveAvailabilityByDoctorId"] ?? "Error desconocido al obtener disponibilidad activa.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}


