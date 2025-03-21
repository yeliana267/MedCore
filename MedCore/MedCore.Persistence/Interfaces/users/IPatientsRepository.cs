

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;
using MedCore.Persistence.Base;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IPatientsRepository : IBaseRepository<Patients, int>
    {
        Task<OperationResult> UpdateEmergencyContactAsync(int patientId, string contactName, string contactPhone); // Actualizar contacto de emergencia
    }
}