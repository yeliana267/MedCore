

using System.Numerics;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.users
{
    public class DoctorsRepository : BaseRepository<Doctors, int>, IDoctorsRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<DoctorsRepository> _logger;
        private readonly IConfiguration _configuration;
        public DoctorsRepository(MedCoreContext context, ILogger<DoctorsRepository> loger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = loger;
            _configuration = configuration;
        }

        public async Task<OperationResult> AssignSpecialtyAsync(int doctorId, int specialtyId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Appointments>> GetDoctorAppointmentsAsync(int doctorId)
        {
            throw new NotImplementedException();
        }
        public async Task<Doctors> GetDoctorByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> SetDoctorAvailabilityAsync(int doctorId, List<DoctorAvailability> availability)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> UpdateDoctorProfileAsync(Doctors doctor)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> UpdateDoctorAsync(int id, Doctors entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctor = await _context.Doctors.FindAsync(id);

                if (doctor == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró el doctor con ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: doctor con id: {id} no encontrado.");
                    return result;
                }

                _logger.LogInformation($"Actualizando doctor con id: {id}");

                doctor.AvailabilityModeId = entity.AvailabilityModeId ?? doctor.AvailabilityModeId;
                doctor.Education = entity.Education ?? doctor.Education;
                doctor.ClinicAddress = entity.ClinicAddress ?? doctor.ClinicAddress;
                doctor.Bio = entity.Bio ?? doctor.Bio;
                doctor.LicenseNumber = entity.LicenseNumber ?? doctor.LicenseNumber;
                doctor.LicenseExpirationDate = entity.LicenseExpirationDate;
                doctor.SpecialtyID = entity.SpecialtyID;
                doctor.ConsultationFee = entity.ConsultationFee ?? doctor.ConsultationFee;
                doctor.PhoneNumber = entity.PhoneNumber ?? doctor.PhoneNumber;
                doctor.YearsOfExperience = entity.YearsOfExperience;


                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = $"Doctor con ID {id} actualizada correctamente.";
                _logger.LogInformation($"Doctor {id} actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el Doctor: {ex.Message}";
                _logger.LogError($"Error en UpdateEntityAsync para el Doctor {id}: {ex.Message}", ex);
            }

            return result;
        }
        public async Task<OperationResult> DeleteDoctorByIdAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM [MedicalAppointment].[users].[Doctors] WHERE DoctorID = {0}", id);

                if (querys > 0)
                {
                    result.Success = true;
                    result.Message = "Doctor eliminado exitosamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontró ningún doctor con ese ID.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el doctor.";
                _logger.LogError($"Error al eliminar el doctor con ID {id}: {ex.Message}", ex);
            }
            return result;
        }

    }

}
