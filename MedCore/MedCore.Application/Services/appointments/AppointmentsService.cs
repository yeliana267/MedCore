using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

        if (appointmentsDto.PatientID <= 0 || appointmentsDto.DoctorID <= 0)
        {
            result.Success = false;
            result.Message = "El paciente y el doctor deben ser válidos.";
            return result;
        }

        if (appointmentsDto.AppointmentDate <= DateTime.Now)
        {
            result.Success = false;
            result.Message = "La fecha de la cita debe ser futura.";
            return result;
        }

        try
        {
            var appointment = new Appointments
            {
                DoctorID = appointmentsDto.DoctorID,
                PatientID = appointmentsDto.PatientID,
                AppointmentDate = appointmentsDto.AppointmentDate,
                StatusID = appointmentsDto.StatusID
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

    public async Task<OperationResult> Update(UpdateAppointmentsDto appointmentsDto)
    {
        var result = new OperationResult();

        if (appointmentsDto == null || appointmentsDto.AppointmentID <= 0)
        {
            result.Success = false;
            result.Message = "Datos inválidos para la actualización.";
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

            existing.AppointmentDate = appointmentsDto.AppointmentDate;
          
            existing.StatusID = appointmentsDto.StatusID;

            return await _appointmentsRepository.UpdateEntityAsync(appointmentsDto.AppointmentID, existing);
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
            return await _appointmentsRepository.DeleteEntityByIdAsync(appointmentsDto.AppointmentID);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error al eliminar la cita: " + ex.Message;
            return result;
        }
    }
}
