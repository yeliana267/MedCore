using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface IMedicalRecordsRepository : IBaseReporsitory<MedicalRecords, int>
    {
    }
}
