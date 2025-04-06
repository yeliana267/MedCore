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
            var result = new OperationResult();

            if (id <= 0)
            {
                result.Success = false;
                result.Message = "El ID proporcionado no es válido.";
                return result;
            }

            try
            {
                var appointment = await _availabilityModesRepository.GetEntityByIdAsync((short)id);
                if (appointment == null)
                {
                    result.Success = false;
                    result.Message = "Cita no encontrada.";
                }
                else
                {
                    result.Success = true;
                    result.Data = appointment;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al buscar la cita: " + ex.Message;
            }

            return result;
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

        public async Task<OperationResult> GetAvailabilityModeByNameAsync(string name)
        {
            var result = new OperationResult();

            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    result.Success = false;
                    result.Message = "El nombre proporcionado no es válido.";
                    return result;
                }

                result = await _availabilityModesRepository.GetAvailabilityModeByNameAsync(name);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error: {ex.Message}";
                _logger.LogError($"Error en GetAvailabilityModeByNameAsync para el modo de disponibilidad {name}: {ex.Message}", ex);
            }

            return result;
        }

    }
}
