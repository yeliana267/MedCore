

using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;

namespace MedCore.Persistence.Repositories.medical
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords, int>, IMedicalRecordsRepository
    {
        public MedicalRecordsRepository(MedCoreContext context) : base(context)
        {
        }
    }
}
