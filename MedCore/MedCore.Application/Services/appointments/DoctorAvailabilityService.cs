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
        OperationResult result = new OperationResult();

        try
        {
            var doctorAvailabilities = await _doctorAvailabilityRepository.GetAllAsync();
            result.Data = doctorAvailabilities;
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
        OperationResult result = new OperationResult();

        if (id <= 0)
        {
            result.Success = false;
            result.Message = "ID inválido.";
            return result;
        }

        try
        {
            var availability = await _doctorAvailabilityRepository.GetEntityByIdAsync(id);
            result.Success = availability != null;
            result.Data = availability;
            result.Message = availability != null ? "Disponibilidad encontrada." : "No se encontró disponibilidad.";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al buscar disponibilidad: {ex.Message}";
            _logger.LogError(result.Message, ex);
        }

        return result;
    }

    public async Task<OperationResult> Save(SaveDoctorAvailabilityDto doctorAvailabilityDto)
    {
        OperationResult result = new OperationResult();

        if (doctorAvailabilityDto == null)
        {
            result.Success = false;
            result.Message = "Datos inválidos.";
            return result;
        }

        if (doctorAvailabilityDto.DoctorID <= 0 || doctorAvailabilityDto.AvailableDate < DateOnly.FromDateTime(DateTime.Now))
        {
            result.Success = false;
            result.Message = "La fecha de disponibilidad debe ser hoy o una fecha futura.";
            return result;
        }

        try
        {
            var availability = new DoctorAvailability
            {
                DoctorID = doctorAvailabilityDto.DoctorID,
                AvailableDate = doctorAvailabilityDto.AvailableDate,
                StartTime = doctorAvailabilityDto.StartTime,
                EndTime = doctorAvailabilityDto.EndTime
            };


            result = await _doctorAvailabilityRepository.SaveEntityAsync(availability);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al guardar disponibilidad: {ex.Message}";
            _logger.LogError(result.Message, ex);
        }

        return result;
    }
    public async Task<OperationResult> Update(UpdateDoctorAvailabilityDto doctorAvailabilityDto)
    {
        OperationResult result = new OperationResult();

        if (doctorAvailabilityDto == null || doctorAvailabilityDto.DoctorID <= 0)
        {
            result.Success = false;
            result.Message = "Datos de actualización inválidos.";
            return result;
        }

        try
        {
            var existing = await _doctorAvailabilityRepository.GetEntityByIdAsync(doctorAvailabilityDto.AvailabilityID);
            if (existing == null)
            {
                result.Success = false;
                result.Message = "Disponibilidad no encontrada.";
                return result;
            }

            existing.DoctorID = doctorAvailabilityDto.DoctorID;
            existing.AvailableDate = doctorAvailabilityDto.AvailableDate;
            existing.StartTime = doctorAvailabilityDto.StartTime;
            existing.EndTime = doctorAvailabilityDto.EndTime;

            result = await _doctorAvailabilityRepository.UpdateEntityAsync(doctorAvailabilityDto.AvailabilityID, existing);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al actualizar: {ex.Message}";
            _logger.LogError(result.Message, ex);
        }

        return result;
    }

    public async Task<OperationResult> Remove(RemoveDoctorAvailabilityDto dto)
    {
        OperationResult result = new OperationResult();

        if (dto == null || dto.AvailabilityID <= 0)
        {
            result.Success = false;
            result.Message = "ID inválido para eliminar.";
            return result;
        }

        try
        {
            result = await _doctorAvailabilityRepository.DeleteEntityByIdAsync(dto.AvailabilityID);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error al eliminar: {ex.Message}";
            _logger.LogError(result.Message, ex);
        }

        return result;
    }
}
