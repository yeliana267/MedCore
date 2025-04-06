using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Medical
{
    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        private readonly ILogger<SpecialtiesService> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesService(ISpecialtiesRepository specialtiesRepository,
            ILogger<SpecialtiesService> logger,
            IConfiguration configuration)
        {
            _specialtiesRepository = specialtiesRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var result = await _specialtiesRepository.GetAllAsync();
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las especialidades.");
                return new OperationResult { Success = false, Message = "Error al obtener todas las especialidades." };
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
                var result = await _specialtiesRepository.GetEntityByIdAsync((short)id);
                if (result == null)
                {
                    return new OperationResult { Success = false, Message = "Especialidad no encontrada." };
                }
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la especialidad por ID.");
                return new OperationResult { Success = false, Message = "Error al obtener la especialidad por ID." };
            }
        }

        public async Task<OperationResult> Remove(RemoveSpecialtiesDto dto)
        {
            if (dto.SpecialtiesId <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var result = await _specialtiesRepository.DeleteEntityByIdAsync(dto.SpecialtiesId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la especialidad.");
                return new OperationResult { Success = false, Message = "Error al eliminar la especialidad." };
            }
        }

        public async Task<OperationResult> Save(SaveSpecialtiesDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SpecialtyName))
            {
                return new OperationResult { Success = false, Message = "El nombre de la especialidad no puede estar vacío." };
            }

            try
            {
                var entity = new Specialties
                {
                    SpecialtyName = dto.SpecialtyName,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = dto.UpdatedAt,
                    IsActive = dto.IsActive
                };

                var result = await _specialtiesRepository.SaveEntityAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la especialidad.");
                return new OperationResult { Success = false, Message = "Error al guardar la especialidad." };
            }
        }

        public async Task<OperationResult> Update(UpdateSpecialtiesDto dto)
        {
            if (dto.SpecialtiesId <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var entity = new Specialties
                {
                    Id = dto.SpecialtiesId,
                    SpecialtyName = dto.SpecialtyName,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = dto.UpdatedAt,
                    IsActive = dto.IsActive
                };

                var result = await _specialtiesRepository.UpdateEntityAsync(entity.Id, entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la especialidad.");
                return new OperationResult { Success = false, Message = "Error al actualizar la especialidad." };
            }
        }

        public async Task<OperationResult> GetSpecialtyByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new OperationResult { Success = false, Message = "El nombre no puede estar vacío." };
            }
            try
            {
                var result = await _specialtiesRepository.GetSpecialtyByNameAsync(name);
                if (result == null)
                {
                    return new OperationResult { Success = false, Message = "Especialidad no encontrada." };
                }
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la especialidad por nombre.");
                return new OperationResult { Success = false, Message = "Error al obtener la especialidad por nombre." };
            }
        }
    }
}