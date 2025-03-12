using MedCore.Application.Dtos.Medical.MedicalRecordsDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Medical
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;
        private readonly ILogger<MedicalRecordsService> _logger;
        private readonly IConfiguration _configuration;

        public MedicalRecordsService(IMedicalRecordsRepository medicalRecordsRepository,
            ILogger<MedicalRecordsService> logger,
            IConfiguration configuration)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var result = await _medicalRecordsRepository.GetAllAsync();
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros médicos.");
                return new OperationResult { Success = false, Message = "Error al obtener todos los registros médicos." };
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            if (id <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var result = await _medicalRecordsRepository.GetEntityByIdAsync(id);
                if (result == null)
                {
                    return new OperationResult { Success = false, Message = "Registro médico no encontrado." };
                }
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el registro médico por ID.");
                return new OperationResult { Success = false, Message = "Error al obtener el registro médico por ID." };
            }
        }

        public async Task<OperationResult> Remove(RemoveMedicalRecordsDto dto)
        {
            if (dto.MedicalRecordsId <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var result = await _medicalRecordsRepository.DeleteEntityByIdAsync(dto.MedicalRecordsId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el registro médico.");
                return new OperationResult { Success = false, Message = "Error al eliminar el registro médico." };
            }
        }

        public async Task<OperationResult> Save(SaveMedicalRecordsDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Diagnosis))
            {
                return new OperationResult { Success = false, Message = "El diagnóstico no puede estar vacío." };
            }

            try
            {
                var entity = new MedicalRecords
                {
                    PatientID = dto.PatientID,
                    DoctorID = dto.DoctorID,
                    Diagnosis = dto.Diagnosis,
                    Treatment = dto.Treatment,
                    DateOfVisit = dto.DateOfVisit,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = dto.UpdatedAt
                };

                var result = await _medicalRecordsRepository.SaveEntityAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro médico.");
                return new OperationResult { Success = false, Message = "Error al guardar el registro médico." };
            }
        }

        public async Task<OperationResult> Update(UpdateMedicalRecordsDto dto)
        {
            if (dto.MedicalRecordsId <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var entity = new MedicalRecords
                {
                    Id = dto.MedicalRecordsId,
                    PatientID = dto.PatientID,
                    DoctorID = dto.DoctorID,
                    Diagnosis = dto.Diagnosis,
                    Treatment = dto.Treatment,
                    DateOfVisit = dto.DateOfVisit,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = dto.UpdatedAt
                };

                var result = await _medicalRecordsRepository.UpdateEntityAsync(entity.Id, entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el registro médico.");
                return new OperationResult { Success = false, Message = "Error al actualizar el registro médico." };
            }
        }
    }
}