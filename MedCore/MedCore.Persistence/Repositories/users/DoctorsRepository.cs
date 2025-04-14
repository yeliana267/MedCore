

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

        public override async Task<OperationResult> UpdateEntityAsync(int id, Doctors entity)
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
        public override async Task<OperationResult> DeleteEntityByIdAsync(int id)
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
        public async Task<OperationResult> GetDoctorsBySpecialtyAsync(int specialtyId)
        {
            try
            {

                if (specialtyId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Specialty ID invalido."
                    };
                }


                var doctors = await _context.Doctors
                    .Where(d => d.SpecialtyID == specialtyId)
                    .ToListAsync();

                if (doctors == null || !doctors.Any())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Doctors no encontrado por especialidad."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "Doctor encontrad.",
                    Data = doctors
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error al buscar doctor: {ex.Message}"
                };
            }
        }
        public async Task<OperationResult> UpdateConsultationFeeAsync(int doctorId, decimal consultationFee)
        {
            try
            {
                if (doctorId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Doctor ID invalido."
                    };
                }

                if (consultationFee < 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Consultation fee no puede ser negativa."
                    };
                }

                var doctor = await _context.Doctors.FindAsync(doctorId);
                if (doctor == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Doctor no encontrado."
                    };
                }

                doctor.ConsultationFee = consultationFee;
                doctor.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "Consultation fee actulizada.",
                    Data = doctor 
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error actualizando consultation fee: {ex.Message}"
                };
            }
        }
        public async Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateTime expirationDate)
        {
            try
            {

                if (expirationDate < DateTime.UtcNow)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Expiration date cannot be in the past."
                    };
                }

                var doctors = await _context.Doctors
                    .Where(d => d.LicenseExpirationDate.ToDateTime(TimeOnly.MinValue) <= expirationDate)
                    .ToListAsync();

                if (doctors == null || !doctors.Any())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "No doctors found with expiring licenses."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "Doctors with expiring licenses retrieved successfully.",
                    Data = doctors
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error retrieving doctors: {ex.Message}"
                };
            }
        }
        public override async Task<OperationResult> SaveEntityAsync(Doctors entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                // Verificar si el User asociado existe
                var userExists = await _context.Users.AnyAsync(u => u.Id == entity.Id);
                if (!userExists)
                {
                    result.Success = false;
                    result.Message = "El usuario asociado no existe.";
                    return result;
                }

                // Verificar si el doctor ya existe
                var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == entity.Id);
                if (doctorExists)
                {
                    result.Success = false;
                    result.Message = "Ya existe un doctor con este ID.";
                    return result;
                }

                await _context.Doctors.AddAsync(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Doctor guardado correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al guardar el doctor: {ex.Message}";
            }
            return result;
        }

    }

}
