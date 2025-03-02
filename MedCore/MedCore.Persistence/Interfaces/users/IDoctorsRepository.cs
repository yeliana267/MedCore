

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IDoctorsRepository : IBaseReporsitory<Doctors, int>
    {
        Task<Doctors> GetDoctorByUserIdAsync(int userId);
        Task<OperationResult> UpdateDoctorProfileAsync(Doctors doctor);
        Task<List<Appointments>> GetDoctorAppointmentsAsync(int doctorId);
        Task<OperationResult> SetDoctorAvailabilityAsync(int doctorId, List<DoctorAvailability> availability);
        Task<OperationResult> AssignSpecialtyAsync(int doctorId, int specialtyId);

        Task<OperationResult> UpdateDoctorAsync(int id, Doctors entity);
        Task<OperationResult> DeleteDoctorByIdAsync(int id);
    }
}
