using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface IMedicalRecordsRepository : IBaseRepository<MedicalRecords, int>
    {
        // Obtiene todos los registros médicos de un paciente específico por su ID
        Task<List<OperationResult>> GetMedicalRecordsByPatientIdAsync(int patientId);

        // Elimina un registro por su ID
        Task<OperationResult> DeleteMedicalRecordAsync(int id);

    }
}
