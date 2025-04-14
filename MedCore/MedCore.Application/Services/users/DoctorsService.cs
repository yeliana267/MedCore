using MedCore.Application.Dtos.users.Doctors;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Model.Models.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MedCore.Application.Services.users
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _doctorsRepository ;
        private readonly ILogger<DoctorsService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;

        public DoctorsService(IDoctorsRepository doctorsRepository, IUsersRepository usersRepository, ILogger<DoctorsService> logger, IConfiguration configuration)
        {
            _doctorsRepository = doctorsRepository;
            _usersRepository = usersRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ActivateDoctorAsync(int doctorId)
        {
            var result = new OperationResult();

            try
            {
                // Validar que el ID del doctor sea válido
                if (doctorId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del doctor no es válido.";
                    return result;
                }

                // Obtener el doctor existente
                var existingDoctor = await _doctorsRepository.GetEntityByIdAsync(doctorId);
                if (existingDoctor == null)
                {
                    result.Success = false;
                    result.Message = "Doctor no encontrado.";
                    return result;
                }

                // Activar al doctor (asumiendo que hay una propiedad IsActive)
                existingDoctor.IsActive = true;

                // Guardar los cambios en la base de datos
                var updateResult = await _doctorsRepository.UpdateEntityAsync(doctorId, existingDoctor);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al activar el doctor: {updateResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Doctor activado correctamente.";
                result.Data = existingDoctor; // Opcional: devolver el doctor actualizado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar el doctor.");
                result.Success = false;
                result.Message = $"Error inesperado al activar el doctor: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> DeactivateDoctorAsync(int doctorId)
        {
            var result = new OperationResult();

            try
            {
                // Validar que el ID del doctor sea válido
                if (doctorId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del doctor no es válido.";
                    return result;
                }

                // Obtener el doctor existente
                var existingDoctor = await _doctorsRepository.GetEntityByIdAsync(doctorId);
                if (existingDoctor == null)
                {
                    result.Success = false;
                    result.Message = "Doctor no encontrado.";
                    return result;
                }

                // Desactivar al doctor (asumiendo que hay una propiedad IsActive)
                existingDoctor.IsActive = false;

                // Guardar los cambios en la base de datos
                var updateResult = await _doctorsRepository.UpdateEntityAsync(doctorId, existingDoctor);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al desactivar el doctor: {updateResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Doctor desactivado correctamente.";
                result.Data = existingDoctor; // Opcional: devolver el doctor actualizado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar el doctor.");
                result.Success = false;
                result.Message = $"Error inesperado al desactivar el doctor: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> GetAll()
        {
            var result = new OperationResult();

            try
            {
                // Obtener todos los doctores
                var doctors = await _doctorsRepository.GetAllAsync();
                if (doctors == null || !doctors.Any())
                {
                    result.Success = false;
                    result.Message = "No se encontraron doctores.";
                    return result;
                }

                result.Success = true;
                result.Message = "Doctores obtenidos correctamente.";
                result.Data = doctors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los doctores.");
                result.Success = false;
                result.Message = $"Error inesperado al obtener los doctores: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> GetById(int id)
        {
            var result = new OperationResult();

            try
            {
                // Validar que el ID sea válido
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del doctor no es válido.";
                    return result;
                }

                // Obtener el doctor por ID
                var doctor = await _doctorsRepository.GetEntityByIdAsync(id);
                if (doctor == null)
                {
                    result.Success = false;
                    result.Message = "Doctor no encontrado.";
                    return result;
                }

                result.Success = true;
                result.Message = "Doctor obtenido correctamente.";
                result.Data = doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el doctor por ID.");
                result.Success = false;
                result.Message = $"Error inesperado al obtener el doctor: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> Remove(RemoveDoctorsDto dto)
        {
            var result = new OperationResult();

            try
            {
                // Validar que el DTO no sea nulo
                if (dto == null)
                {
                    result.Success = false;
                    result.Message = "El DTO no puede ser nulo.";
                    return result;
                }

                // Validar que el ID del doctor sea válido
                if (dto.DoctorID <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del doctor no es válido.";
                    return result;
                }

                // Eliminar el doctor
                var removeResult = await _doctorsRepository.DeleteEntityByIdAsync(dto.DoctorID);
                if (!removeResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al eliminar el doctor: {removeResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Doctor eliminado correctamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el doctor.");
                result.Success = false;
                result.Message = $"Error inesperado al eliminar el doctor: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> Save(SaveDoctorsDto dto)
        {
            var result = new OperationResult();

            try
            {
                
                if (dto == null)
                {
                    result.Success = false;
                    result.Message = "El DTO no puede ser nulo.";
                    return result;
                }

                
                var user = await _usersRepository.GetEntityByIdAsync(dto.UserID);
                if (user == null)
                {
                    result.Success = false;
                    result.Message = "El usuario asociado no existe.";
                    return result;
                }

               
                if (string.IsNullOrEmpty(dto.LicenseNumber) || dto.AvailabilityModeId == null || dto.LicenseExpirationDate == default)
                {
                    result.Success = false;
                    result.Message = "Número de licencia, modo de disponibilidad y fecha de expiración son campos obligatorios.";
                    return result;
                }

               
                var doctor = new Doctors
                {
                    Id = dto.UserID, 
                    SpecialtyID = dto.SpecialtyID,
                    LicenseNumber = dto.LicenseNumber,
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    AvailabilityModeId = dto.AvailabilityModeId,
                    LicenseExpirationDate = dto.LicenseExpirationDate,
                    IsActive = dto.IsActive
                };

                
                var saveResult = await _doctorsRepository.SaveEntityAsync(doctor);
                if (!saveResult.Success)
                {
                    return saveResult; 
                }

                result.Success = true;
                result.Message = "Doctor guardado correctamente.";
                result.Data = doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el doctor.");
                result.Success = false;
                result.Message = $"Error inesperado al guardar el doctor: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> Update(UpdateDoctorsDto dto)
        {
            var result = new OperationResult();

            if (dto == null || dto.DoctorID <= 0)
            {
                result.Success = false;
                result.Message = "Datos inválidos para la actualización.";
                return result;
            }

            try
            {
                var existing = await _doctorsRepository.GetEntityByIdAsync(dto.DoctorID);
                if (existing == null)
                {
                    result.Success = false;
                    result.Message = "Doctor no encontrado.";
                    return result;
                }

               
                if (!existing.IsActive && dto.IsActive.HasValue && !dto.IsActive.Value)
                {
                    result.Success = false;
                    result.Message = "No se pueden modificar doctores inactivos.";
                    return result;
                }

               
                if (dto.LicenseNumber != null) existing.LicenseNumber = dto.LicenseNumber;
                if (dto.PhoneNumber != null) existing.PhoneNumber = dto.PhoneNumber;
                if (dto.YearsOfExperience.HasValue) existing.YearsOfExperience = dto.YearsOfExperience.Value;
                if (dto.Education != null) existing.Education = dto.Education;
                if (dto.Bio != null) existing.Bio = dto.Bio;
                if (dto.ConsultationFee.HasValue) existing.ConsultationFee = dto.ConsultationFee.Value;
                if (dto.ClinicAddress != null) existing.ClinicAddress = dto.ClinicAddress;
                if (dto.AvailabilityModeId.HasValue) existing.AvailabilityModeId = dto.AvailabilityModeId.Value;
                if (dto.LicenseExpirationDate.HasValue) existing.LicenseExpirationDate = dto.LicenseExpirationDate.Value;
                if (dto.IsActive.HasValue) existing.IsActive = dto.IsActive.Value;
                if (dto.SpecialtyID.HasValue) existing.SpecialtyID = (short)dto.SpecialtyID.Value;

                existing.UpdatedAt = DateTime.UtcNow;

                return await _doctorsRepository.UpdateEntityAsync(dto.DoctorID, existing);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al actualizar el doctor: " + ex.Message;
                return result;
            }
        }
        public async Task<OperationResult> UpdateConsultationFeeAsync(int doctorId, decimal consultationFee)
        {
            var result = new OperationResult();

            try
            {
                // Validar que el ID del doctor sea válido
                if (doctorId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del doctor no es válido.";
                    return result;
                }

                // Validar que la tarifa de consulta sea válida
                if (consultationFee <= 0)
                {
                    result.Success = false;
                    result.Message = "La tarifa de consulta debe ser mayor que cero.";
                    return result;
                }

                // Obtener el doctor existente
                var existingDoctor = await _doctorsRepository.GetEntityByIdAsync(doctorId);
                if (existingDoctor == null)
                {
                    result.Success = false;
                    result.Message = "Doctor no encontrado.";
                    return result;
                }

                // Actualizar la tarifa de consulta
                existingDoctor.ConsultationFee = consultationFee;

                // Guardar los cambios en la base de datos
                var updateResult = await _doctorsRepository.UpdateEntityAsync(doctorId, existingDoctor);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al actualizar la tarifa de consulta: {updateResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Tarifa de consulta actualizada correctamente.";
                result.Data = existingDoctor; // Opcional: devolver el doctor actualizado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la tarifa de consulta.");
                result.Success = false;
                result.Message = $"Error inesperado al actualizar la tarifa de consulta: {ex.Message}";
            }

            return result;
        }
    }
}
