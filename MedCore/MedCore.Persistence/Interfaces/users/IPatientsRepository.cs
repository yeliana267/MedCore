

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IPatientsRepository : IBaseRepository<Patients, int>
    {
        Task<Patients> GetPatientByUserIdAsync(int userId);
        Task<OperationResult> UpdatePatientProfileAsync(Patients patient);
        Task<List<Appointments>> GetPatientAppointmentsAsync(int patientId);
        Task<OperationResult> ScheduleAppointmentAsync(Appointments appointment);
        Task<OperationResult> CancelAppointmentAsync(int appointmentId);
        Task<OperationResult> UpdatePatientAsync(int id, Patients entity);
        Task<OperationResult> DeletePatientByIdAsync(int id);
    }
}