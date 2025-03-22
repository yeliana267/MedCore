using MedCore.Application.Dtos.users.Doctors;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
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

        public DoctorsService(IDoctorsRepository doctorsRepository, ILogger<DoctorsService> logger, IConfiguration configuration)
        {
            _doctorsRepository = doctorsRepository;
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
                // Validar que el DTO no sea nulo
                if (dto == null)
                {
                    result.Success = false;
                    result.Message = "El DTO no puede ser nulo.";
                    return result;
                }

                // Validar campos obligatorios
                if (string.IsNullOrEmpty(dto.LicenseNumber) || dto.AvailabilityModeId == null || dto.LicenseExpirationDate == default)
                {
                    result.Success = false;
                    result.Message = "Número de licencia, modo de disponibilidad y fecha de expiración son campos obligatorios.";
                    return result;
                }

                // Crear la entidad Doctors
                var doctor = new Doctors
                {
                    SpecialtyID = dto.SpecialtyID, // short
                    LicenseNumber = dto.LicenseNumber, // string
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    AvailabilityModeId = dto.AvailabilityModeId, // short?
                    LicenseExpirationDate = dto.LicenseExpirationDate, // DateOnly
                    IsActive = dto.IsActive
                };

                // Guardar el doctor en la base de datos
                await _doctorsRepository.SaveEntityAsync(doctor);

                result.Success = true;
                result.Message = "Doctor guardado correctamente.";
                result.Data = doctor; // Opcional: devolver el doctor creado
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

                // Obtener el doctor existente
                var existingDoctor = await _doctorsRepository.GetEntityByIdAsync(dto.DoctorID);
                if (existingDoctor == null)
                {
                    result.Success = false;
                    result.Message = "Doctor no encontrado.";
                    return result;
                }

                // Actualizar los campos del doctor
                existingDoctor.SpecialtyID = dto.SpecialtyID; // short
                existingDoctor.LicenseNumber = dto.LicenseNumber; // string
                existingDoctor.PhoneNumber = dto.PhoneNumber;
                existingDoctor.YearsOfExperience = dto.YearsOfExperience;
                existingDoctor.Education = dto.Education;
                existingDoctor.Bio = dto.Bio;
                existingDoctor.ConsultationFee = dto.ConsultationFee;
                existingDoctor.ClinicAddress = dto.ClinicAddress;
                existingDoctor.AvailabilityModeId = dto.AvailabilityModeId; // short?
                existingDoctor.LicenseExpirationDate = dto.LicenseExpirationDate; // DateOnly
                existingDoctor.IsActive = dto.IsActive;

                // Guardar los cambios en la base de datos
                await _doctorsRepository.UpdateEntityAsync(dto.DoctorID, existingDoctor);

                result.Success = true;
                result.Message = "Doctor actualizado correctamente.";
                result.Data = existingDoctor; // Opcional: devolver el doctor actualizado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el doctor.");
                result.Success = false;
                result.Message = $"Error inesperado al actualizar el doctor: {ex.Message}";
            }

            return result;
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
