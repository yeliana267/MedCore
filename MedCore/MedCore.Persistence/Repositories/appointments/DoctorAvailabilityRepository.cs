using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        public async Task<OperationResult> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
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

        public override async Task<OperationResult> UpdateEntityAsync(int id, DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctorAvailability = await _context.DoctorAvailabilities.FindAsync(id);

                if (doctorAvailability == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontraron medicos disponibles del ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: Disponibilidad medica {id} no encontrada.");
                    return result;
                }

                _logger.LogInformation($"Actualizando cita medica {id}");

                doctorAvailability.AvailableDate = entity.AvailableDate;
                doctorAvailability.DoctorID = entity.DoctorID;
                doctorAvailability.StartTime = entity.StartTime;
                doctorAvailability.EndTime = entity.EndTime;

                _context.DoctorAvailabilities.Update(doctorAvailability);
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


