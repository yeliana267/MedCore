

using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IPatientsRepository : IBaseReporsitory<Patients, int>
    {
        Task<OperationResult> UpdatePatientProfileAsync(Patients patient);
    }
}
