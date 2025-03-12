using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Medical
{
    public class AvailabilityModesService : IAvailabilityModesService
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository;
        private readonly ILogger<AvailabilityModesService> _logger;
        private readonly IConfiguration _configuration;

        public AvailabilityModesService(IAvailabilityModesRepository availabilityModesRepository,
            ILogger<AvailabilityModesService> logger,
            IConfiguration configuration)
        {
            _availabilityModesRepository = availabilityModesRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var result = await _availabilityModesRepository.GetAllAsync();
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los modos de disponibilidad.");
                return new OperationResult { Success = false, Message = "Error al obtener todos los modos de disponibilidad." };
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
                var result = await _availabilityModesRepository.GetEntityByIdAsync((short)id);
                if (result == null)
                {
                    return new OperationResult { Success = false, Message = "Modo de disponibilidad no encontrado." };
                }
                return new OperationResult { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el modo de disponibilidad por ID.");
                return new OperationResult { Success = false, Message = "Error al obtener el modo de disponibilidad por ID." };
            }
        }

        public async Task<OperationResult> Remove(RemoveAvailabilityModesDto dto)
        {
            if (dto.AvailabilityModesId <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var result = await _availabilityModesRepository.DeleteEntityByIdAsync(dto.AvailabilityModesId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el modo de disponibilidad.");
                return new OperationResult { Success = false, Message = "Error al eliminar el modo de disponibilidad." };
            }
        }

        public async Task<OperationResult> Save(SaveAvailibilityModesDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.AvailabilityMode))
            {
                return new OperationResult { Success = false, Message = "El modo de disponibilidad no puede estar vacío." };
            }

            try
            {
                var entity = new AvailabilityModes
                {
                    AvailabilityMode = dto.AvailabilityMode,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = dto.UpdatedAt,
                    IsActive = dto.IsActive
                };

                var result = await _availabilityModesRepository.SaveEntityAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el modo de disponibilidad.");
                return new OperationResult { Success = false, Message = "Error al guardar el modo de disponibilidad." };
            }
        }

        public async Task<OperationResult> Update(UpdateAvailibilityModesDto dto)
        {
            if (dto.AvailabilityModesId <= 0)
            {
                return new OperationResult { Success = false, Message = "El ID debe ser un número positivo." };
            }

            try
            {
                var entity = new AvailabilityModes
                {
                    Id = dto.AvailabilityModesId,
                    AvailabilityMode = dto.AvailabilityMode,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = dto.UpdatedAt,
                    IsActive = dto.IsActive
                };

                var result = await _availabilityModesRepository.UpdateEntityAsync(entity.Id, entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el modo de disponibilidad.");
                return new OperationResult { Success = false, Message = "Error al actualizar el modo de disponibilidad." };
            }
        }
    }
}