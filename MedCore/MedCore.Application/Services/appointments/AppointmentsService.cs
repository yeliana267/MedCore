using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;


public class AppointmentsService : IAppointmentsService
{
    private readonly ILogger<AppointmentsService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsService(
        IAppointmentsRepository appointmentsRepository,
        ILogger<AppointmentsService> logger,
        IConfiguration configuration)
    {
        _appointmentsRepository = appointmentsRepository;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<OperationResult> GetAll()
    {
        var result = new OperationResult();
        try
        {
            var data = await _appointmentsRepository.GetAllAsync();
            result.Data = data;
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error al obtener citas: " + ex.Message;
        }

        return result;
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
            var appointment = await _appointmentsRepository.GetEntityByIdAsync(id);
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

    public async Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        if (doctorId <= 0)
        {
            return new OperationResult { Success = false, Message = "ID de doctor inválido." };
        }

        return await _appointmentsRepository.GetAppointmentsByDoctorIdAsync(doctorId);
    }

    public async Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId)
    {
        if (patientId <= 0)
        {
            return new OperationResult { Success = false, Message = "ID de paciente inválido." };
        }

        return await _appointmentsRepository.GetAppointmentsByPatientIdAsync(patientId);
    }

    public async Task<OperationResult> GetPendingAppointmentsAsync()
    {
        return await _appointmentsRepository.GetPendingAppointmentsAsync();
    }

    public async Task<OperationResult> Save(SaveAppointmentsDto appointmentsDto)
    {
        var result = new OperationResult();

        if (appointmentsDto == null)
        {
            result.Success = false;
            result.Message = "Los datos de la cita no pueden estar vacíos.";
            return result;
        }

        if (appointmentsDto.PatientID <= 0 || appointmentsDto.DoctorID <= 0 || appointmentsDto.StatusID <= 0)
        {
            result.Success = false;
            result.Message = "El paciente, el doctor y el estado deben ser números válidos.";
            return result;
        }

        if (appointmentsDto.AppointmentDate <= DateTime.Now)
        {
            result.Success = false;
            result.Message = "La fecha de la cita debe ser futura.";
            return result;
        }

        if (appointmentsDto.AppointmentDate.TimeOfDay < new TimeSpan(8, 0, 0) ||
            appointmentsDto.AppointmentDate.TimeOfDay > new TimeSpan(18, 0, 0))
        {
            result.Success = false;
            result.Message = "Las citas solo pueden programarse entre 8:00 AM y 6:00 PM.";
            return result;
        }

        if (appointmentsDto.AppointmentDate.DayOfWeek == DayOfWeek.Saturday ||
            appointmentsDto.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
        {
            result.Success = false;
            result.Message = "No se pueden programar citas los fines de semana.";
            return result;
        }

        if (appointmentsDto.AppointmentDate.Minute % 30 != 0)
        {
            result.Success = false;
            result.Message = "Las citas deben programarse en intervalos de 30 minutos.";
            return result;
        }

        try
        {
            var appointment = new Appointments
            {
                DoctorID = appointmentsDto.DoctorID,
                PatientID = appointmentsDto.PatientID,
                AppointmentDate = appointmentsDto.AppointmentDate,
                StatusID = appointmentsDto.StatusID,
            };

            return await _appointmentsRepository.SaveEntityAsync(appointment);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error al guardar la cita: " + ex.Message;
            return result;
        }
    }

    public async Task<OperationResult> Update(UpdateAppointmentsDto dto)
    {
        var result = new OperationResult();

        if (dto == null || dto.AppointmentID <= 0)
        {
            result.Success = false;
            result.Message = "Datos inválidos para la actualización.";
            return result;
        }

        try
        {
            var existing = await _appointmentsRepository.GetEntityByIdAsync(dto.AppointmentID);
            if (existing == null)
            {
                result.Success = false;
                result.Message = "Cita no encontrada.";
                return result;
            }

            // Validación de negocio
            if (existing.StatusID == 3)
            {
                result.Success = false;
                result.Message = "No se pueden modificar citas completadas.";
                return result;
            }

            if (dto.AppointmentDate.HasValue && dto.AppointmentDate.Value < DateTime.Now.AddHours(24))
            {
                result.Success = false;
                result.Message = "Se requiere 24h de anticipación para modificaciones.";
                return result;
            }

            // Validaciones adicionales como en Save (opcional)
            if (dto.AppointmentDate.HasValue)
            {
                if (dto.AppointmentDate.Value <= DateTime.Now)
                {
                    result.Success = false;
                    result.Message = "La nueva fecha de cita debe ser futura.";
                    return result;
                }

                if (dto.AppointmentDate.Value.TimeOfDay < new TimeSpan(8, 0, 0) ||
                    dto.AppointmentDate.Value.TimeOfDay > new TimeSpan(18, 0, 0))
                {
                    result.Success = false;
                    result.Message = "Las citas solo pueden programarse entre 8:00 AM y 6:00 PM.";
                    return result;
                }

                if (dto.AppointmentDate.Value.DayOfWeek == DayOfWeek.Saturday ||
                    dto.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    result.Success = false;
                    result.Message = "No se pueden programar citas los fines de semana.";
                    return result;
                }

                if (dto.AppointmentDate.Value.Minute % 30 != 0)
                {
                    result.Success = false;
                    result.Message = "Las citas deben programarse en intervalos de 30 minutos.";
                    return result;
                }
            }

            // Actualización condicional
            if (dto.AppointmentDate.HasValue) existing.AppointmentDate = dto.AppointmentDate.Value;
            if (dto.StatusID.HasValue) existing.StatusID = dto.StatusID.Value;
            if (dto.PatientID.HasValue) existing.PatientID = dto.PatientID.Value;
            if (dto.DoctorID.HasValue) existing.DoctorID = dto.DoctorID.Value;

            existing.UpdatedAt = DateTime.UtcNow;

            return await _appointmentsRepository.UpdateEntityAsync(dto.AppointmentID, existing);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error al actualizar la cita: " + ex.Message;
            return result;
        }
    }

    public async Task<OperationResult> Remove(RemoveAppointmentsDto appointmentsDto)
    {
        var result = new OperationResult();

        if (appointmentsDto == null || appointmentsDto.AppointmentID <= 0)
        {
            result.Success = false;
            result.Message = "ID inválido para eliminar la cita.";
            return result;
        }

        try
        {
            var existing = await _appointmentsRepository.GetEntityByIdAsync(appointmentsDto.AppointmentID);
            if (existing == null)
            {
                result.Success = false;
                result.Message = "Cita no encontrada.";
                return result;
            }

            //if (existing.AppointmentDate < DateTime.Now.AddHours(48))
            //{
            //    result.Success = false;
            //    result.Message = "No se pueden cancelar citas con menos de 48 horas de anticipación.";
            //    return result;
            //}

            if (existing.StatusID == 4)
            {
                result.Success = false;
                result.Message = "La cita ya está cancelada.";
                return result;
            }

            return await _appointmentsRepository.DeleteEntityByIdAsync(appointmentsDto.AppointmentID);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error al eliminar la cita: " + ex.Message;
            return result;
        }
    }

    private bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) &&
               Regex.IsMatch(name, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
    }
}