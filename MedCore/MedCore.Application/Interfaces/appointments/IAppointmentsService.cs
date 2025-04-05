

using MedCore.Application.Base;
using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Domain.Base;

namespace MedCore.Application.Interfaces.appointments
{
    public interface IAppointmentsService : IBaseService<SaveAppointmentsDto, UpdateAppointmentsDto,RemoveAppointmentsDto>
    {
        Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId); //todas las citas de un paciente específico
        Task<OperationResult> GetPendingAppointmentsAsync(); //Obtener citas pendientes

    }
}
