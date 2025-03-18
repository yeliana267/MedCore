

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IPatientsRepository : IBaseReporsitory<Patients, int>
    {
        Task<OperationResult> GetPatientsByDoctorIdAsync(int doctorId); // Obtener pacientes por doctor
        Task<OperationResult> GetPatientsByBloodTypeAsync(string bloodType); // Obtener pacientes por tipo de sangre
        Task<OperationResult> UpdateEmergencyContactAsync(int patientId, string contactName, string contactPhone); // Actualizar contacto de emergencia
    }
}