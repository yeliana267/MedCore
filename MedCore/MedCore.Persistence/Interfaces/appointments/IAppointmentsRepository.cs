

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Repository;


namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IAppointmentsRepository : IBaseRepository<Appointments, int>
    {
        Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId); //todas las citas de un paciente específico
        Task<OperationResult> GetPendingAppointmentsAsync(); //Obtener citas pendientes
    }
}
