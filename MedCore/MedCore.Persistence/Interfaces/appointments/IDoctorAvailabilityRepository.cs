

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IDoctorAvailabilityRepository : IBaseRepository<DoctorAvailability, int>
    {
        Task<OperationResult> GetAvailabilityByDoctorIdAsync(int doctorId, DateTime startDate, DateTime endDate); //disponibilidad de un doctor en un rango de fechas
        Task<OperationResult> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate, TimeOnly startTime, TimeOnly endTime); //Verificar si un doctor está disponible en una fecha y hora específica
        Task<OperationResult> GetActiveAvailabilityByDoctorIdAsync(int doctorId); // todas las disponibilidades activas de un doctor



    }
}
