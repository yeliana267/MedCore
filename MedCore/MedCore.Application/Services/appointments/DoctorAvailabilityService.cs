using MedCore.Application.Dtos.appointments.DoctorAvailability;
using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class DoctorAvailabilityService : IDoctorAvailabilityService
{
    private readonly ILogger<DoctorAvailabilityService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;

    public DoctorAvailabilityService(
        IDoctorAvailabilityRepository doctorAvailabilityRepository,
        ILogger<DoctorAvailabilityService> logger,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _doctorAvailabilityRepository = doctorAvailabilityRepository;
    }

    public async Task<OperationResult> GetAll()
    {
        var result = new OperationResult();
        try
        {
            var data = await _doctorAvailabilityRepository.GetAllAsync();
            result.Data = data;
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al obtener disponibilidades: {ex.Message}";
            _logger.LogError(result.Message, ex);
        }
        return result;
    }

    public async Task<OperationResult> GetById(int id)
    {
        var result = new OperationResult();

        if (id <= 0)
        {
            result.Success = false;
            result.Message = "ID inválido.";
            return result;
        }

        try
        {
            var availability = await _doctorAvailabilityRepository.GetEntityByIdAsync(id);
            if (availability == null)
            {
                result.Success = false;
                result.Message = "Disponibilidad no encontrada.";
            }
            else
            {
                result.Success = true;
                result.Data = availability;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al buscar disponibilidad: {ex.Message}";
            _logger.LogError(result.Message, ex);
        }

        return result;
    }

    public async Task<OperationResult> Save(SaveDoctorAvailabilityDto dto)
    {
        var result = new OperationResult();

        if (dto == null)
        {
            result.Success = false;
            result.Message = "Los datos de disponibilidad no pueden estar vacíos.";
            return result;
        }

        if (dto.DoctorID <= 0)
        {
            result.Success = false;
            result.Message = "ID de doctor inválido.";
            return result;
        }

        if (dto.AvailableDate < DateOnly.FromDateTime(DateTime.Now))
        {
            result.Success = false;
            result.Message = "La fecha de disponibilidad debe ser hoy o una fecha futura.";
            return result;
        }

        if (dto.StartTime >= dto.EndTime)
        {
            result.Success = false;
            result.Message = "La hora de inicio debe ser anterior a la hora de fin.";
            return result;
        }

        try
        {
            var availability = new DoctorAvailability
            {
                DoctorID = dto.DoctorID,
                AvailableDate = dto.AvailableDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            return await _doctorAvailabilityRepository.SaveEntityAsync(availability);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al guardar disponibilidad: {ex.Message}";
            _logger.LogError(result.Message, ex);
            return result;
        }
    }

    public async Task<OperationResult> Update(UpdateDoctorAvailabilityDto dto)
    {
        var result = new OperationResult();

        if (dto == null || dto.AvailabilityID <= 0)
        {
            result.Success = false;
            result.Message = "Datos inválidos para la actualización.";
            return result;
        }

        try
        {
            var existing = await _doctorAvailabilityRepository.GetEntityByIdAsync(dto.AvailabilityID);
            if (existing == null)
            {
                result.Success = false;
                result.Message = "Disponibilidad no encontrada.";
                return result;
            }

            // Validación de fecha
            if (dto.AvailableDate < DateOnly.FromDateTime(DateTime.Now))
            {
                result.Success = false;
                result.Message = "La fecha de disponibilidad debe ser hoy o una fecha futura.";
                return result;
            }

            // Validación de horario
            if (dto.StartTime >= dto.EndTime)
            {
                result.Success = false;
                result.Message = "La hora de inicio debe ser anterior a la hora de fin.";
                return result;
            }

            // Actualización condicional (ahora con campos nullables)
            if (dto.DoctorID.HasValue) existing.DoctorID = dto.DoctorID.Value;
            if (dto.AvailableDate.HasValue) existing.AvailableDate = dto.AvailableDate.Value;
            if (dto.StartTime.HasValue) existing.StartTime = dto.StartTime.Value;
            if (dto.EndTime.HasValue) existing.EndTime = dto.EndTime.Value;

            return await _doctorAvailabilityRepository.UpdateEntityAsync(dto.AvailabilityID, existing);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al actualizar disponibilidad: {ex.Message}";
            _logger.LogError(result.Message, ex);
            return result;
        }
    }

    public async Task<OperationResult> Remove(RemoveDoctorAvailabilityDto dto)
    {
        var result = new OperationResult();

        if (dto == null || dto.AvailabilityID <= 0)
        {
            result.Success = false;
            result.Message = "ID inválido para eliminar.";
            return result;
        }

        try
        {
            var existing = await _doctorAvailabilityRepository.GetEntityByIdAsync(dto.AvailabilityID);
            if (existing == null)
            {
                result.Success = false;
                result.Message = "Disponibilidad no encontrada.";
                return result;
            }

            // Validación adicional: No permitir eliminar disponibilidades pasadas
            if (existing.AvailableDate < DateOnly.FromDateTime(DateTime.Now))
            {
                result.Success = false;
                result.Message = "No se pueden eliminar disponibilidades pasadas.";
                return result;
            }

            return await _doctorAvailabilityRepository.DeleteEntityByIdAsync(dto.AvailabilityID);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al eliminar disponibilidad: {ex.Message}";
            _logger.LogError(result.Message, ex);
            return result;
        }
    }
}