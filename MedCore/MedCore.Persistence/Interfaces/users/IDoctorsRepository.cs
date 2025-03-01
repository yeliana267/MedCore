

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IDoctorsRepository : IBaseReporsitory<Doctors, int>
    {
        Task<OperationResult> UpdateDoctorProfileAsync(Doctors doctor);

    }
}
