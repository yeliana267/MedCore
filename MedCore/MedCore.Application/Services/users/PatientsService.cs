using MedCore.Application.Dtos.users.Patients;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace MedCore.Application.Services.users
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;

        public PatientsService(IPatientsRepository patientsRepository, IUsersRepository usersRepository, ILogger<PatientsService> logger, IConfiguration configuration)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
            _configuration = configuration;
            _usersRepository = usersRepository;
        }

        public async Task<OperationResult> ActivatePatientAsync(int patientId)
        {
            var result = new OperationResult();

            try
            {
  
                if (patientId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no es válido.";
                    return result;
                }


                var existingPatient = await _patientsRepository.GetEntityByIdAsync(patientId);
                if (existingPatient == null)
                {
                    result.Success = false;
                    result.Message = "Paciente no encontrado.";
                    return result;
                }


                existingPatient.IsActive = true;


                var updateResult = await _patientsRepository.UpdateEntityAsync(patientId, existingPatient);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al activar el paciente: {updateResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Paciente activado correctamente.";
                result.Data = existingPatient; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar el paciente.");
                result.Success = false;
                result.Message = $"Error inesperado al activar el paciente: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> DeactivatePatientAsync(int patientId)
        {
            var result = new OperationResult();

            try
            {
                
                if (patientId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no es válido.";
                    return result;
                }

                
                var existingPatient = await _patientsRepository.GetEntityByIdAsync(patientId);
                if (existingPatient == null)
                {
                    result.Success = false;
                    result.Message = "Paciente no encontrado.";
                    return result;
                }

                
                existingPatient.IsActive = false;

                
                var updateResult = await _patientsRepository.UpdateEntityAsync(patientId, existingPatient);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al desactivar el paciente: {updateResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Paciente desactivado correctamente.";
                result.Data = existingPatient; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar el paciente.");
                result.Success = false;
                result.Message = $"Error inesperado al desactivar el paciente: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var patients = await _patientsRepository.GetAllAsync();

                
                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    result.Message = "No se encontraron pacientes.";
                    _logger.LogWarning("No se encontraron pacientes en la base de datos.");
                }
                else
                {
                    result.Data = patients;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al obtener todos los pacientes: {ex.Message}", ex);
            }
            return result;
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no es válido.";
                    _logger.LogWarning("Intento de obtener paciente fallido: ID no válido.");
                    return result;
                }

                var patient = await _patientsRepository.GetEntityByIdAsync(id);

                
                if (patient == null)
                {
                    result.Success = false;
                    result.Message = "Paciente no encontrado.";
                    _logger.LogWarning($"Paciente con ID {id} no encontrado.");
                }
                else
                {
                    result.Data = patient;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al obtener el paciente con ID {id}: {ex.Message}", ex);
            }
            return result;
        }

        public async Task<OperationResult> Remove(RemovePatientsDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
               
                if (dto.PatientID <= 0) 
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no es válido.";
                    _logger.LogWarning("Intento de eliminación fallido: ID de paciente no válido.");
                    return result;
                }

                
                var patient = await _patientsRepository.GetEntityByIdAsync(dto.PatientID);
                if (patient == null)
                {
                    result.Success = false;
                    result.Message = "Paciente no encontrado.";
                    _logger.LogWarning($"Intento de eliminación fallido: Paciente con ID {dto.PatientID} no encontrado.");
                    return result;
                }

                
                var deleteResult = await _patientsRepository.DeleteEntityByIdAsync(dto.PatientID);
                if (deleteResult.Success)
                {
                    result.Success = true;
                    result.Message = "Paciente eliminado correctamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = deleteResult.Message;
                    _logger.LogWarning($"Error al eliminar el paciente con ID {dto.PatientID}: {deleteResult.Message}");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al eliminar el paciente con ID {dto.PatientID}: {ex.Message}", ex);
            }
            return result;
        }

        public async Task<OperationResult> Save(SavePatientsDto dto)
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

                
                if (dto.DateOfBirth == default || dto.Gender == '\0' || string.IsNullOrEmpty(dto.PhoneNumber))
                {
                    result.Success = false;
                    result.Message = "Fecha de nacimiento, género y número de teléfono son campos obligatorios.";
                    return result;
                }

                
                var patient = new Patients
                {
                    Id = dto.UserID,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    InsuranceProviderID = dto.InsuranceProviderID
                };

               
                var saveResult = await _patientsRepository.SaveEntityAsync(patient);
                if (!saveResult.Success)
                {
                    return saveResult; 
                }

                result.Success = true;
                result.Message = "Paciente guardado correctamente.";
                result.Data = patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el paciente.");
                result.Success = false;
                result.Message = $"Error inesperado al guardar el paciente: {ex.Message}";
            }

            return result;
        }
        public async Task<OperationResult> Update(UpdatePatientsDto dto)
        {
            var result = new OperationResult();

            if (dto == null || dto.PatientID <= 0)
            {
                result.Success = false;
                result.Message = "Datos inválidos para la actualización.";
                return result;
            }

            try
            {
                var existing = await _patientsRepository.GetEntityByIdAsync(dto.PatientID);
                if (existing == null)
                {
                    result.Success = false;
                    result.Message = "Paciente no encontrado.";
                    return result;
                }



                // Actualización condicional IDÉNTICA al ejemplo de Appointments
                if (dto.DateOfBirth.HasValue) existing.DateOfBirth = dto.DateOfBirth.Value;
                if (dto.Gender.HasValue) existing.Gender = dto.Gender.Value;
                if (dto.PhoneNumber != null) existing.PhoneNumber = dto.PhoneNumber;
                if (dto.Address != null) existing.Address = dto.Address;
                if (dto.EmergencyContactName != null) existing.EmergencyContactName = dto.EmergencyContactName;
                if (dto.EmergencyContactPhone != null) existing.EmergencyContactPhone = dto.EmergencyContactPhone;
                if (dto.BloodType != null) existing.BloodType = dto.BloodType;
                if (dto.Allergies != null) existing.Allergies = dto.Allergies;
                if (dto.InsuranceProviderID.HasValue) existing.InsuranceProviderID = dto.InsuranceProviderID.Value;

                existing.UpdatedAt = DateTime.UtcNow;

                return await _patientsRepository.UpdateEntityAsync(dto.PatientID, existing);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al actualizar el paciente: " + ex.Message;
                return result;
            }
        }

        public async Task<OperationResult> UpdateEmergencyContactAsync(int patientId, string contactName, string contactPhone)
        {
            var result = new OperationResult();

            try
            {
                
                if (patientId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no es válido.";
                    return result;
                }

                
                if (string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(contactPhone))
                {
                    result.Success = false;
                    result.Message = "El nombre y el teléfono del contacto de emergencia son obligatorios.";
                    return result;
                }

                
                var existingPatient = await _patientsRepository.GetEntityByIdAsync(patientId);
                if (existingPatient == null)
                {
                    result.Success = false;
                    result.Message = "Paciente no encontrado.";
                    return result;
                }

                
                existingPatient.EmergencyContactName = contactName;
                existingPatient.EmergencyContactPhone = contactPhone;

                
                var updateResult = await _patientsRepository.UpdateEntityAsync(patientId, existingPatient);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = $"Error al actualizar el contacto de emergencia: {updateResult.Message}";
                    return result;
                }

                result.Success = true;
                result.Message = "Contacto de emergencia actualizado correctamente.";
                result.Data = existingPatient; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el contacto de emergencia.");
                result.Success = false;
                result.Message = $"Error inesperado al actualizar el contacto de emergencia: {ex.Message}";
            }

            return result;
        }
    }
    }
