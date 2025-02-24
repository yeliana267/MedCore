using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface IMedicalRecordsRepository : IBaseReporsitory<MedicalRecords, int>
    {
        // Obtiene todos los registros médicos de un paciente específico por su ID
        Task<List<OperationResult>> GetMedicalRecordsByPatientIdAsync(int patientId);

        // Obtiene los registros médicos dentro de un rango de fechas determinado  
        Task<List<OperationResult>> GetMedicalRecordsByDateRangeAsync(DateTime startDate, DateTime endDate);


    }
}
