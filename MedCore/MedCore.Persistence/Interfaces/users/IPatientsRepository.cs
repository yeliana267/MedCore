

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IPatientsRepository : IBaseReporsitory<Patients, int>
    {
        Task<OperationResult> UpdateEmergencyContactAsync(int patientId, string contactName, string contactPhone); // Actualizar contacto de emergencia
    }
}