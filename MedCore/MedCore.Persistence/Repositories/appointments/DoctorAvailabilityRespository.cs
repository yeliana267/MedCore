

using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;

namespace MedCore.Persistence.Repositories.appointments
{
    public class DoctorAvailabilityRespository : BaseRepository<DoctorAvailability, int>, IDoctorAvailabilityRepository
    {
        public DoctorAvailabilityRespository(MedCoreContext context) : base(context)
        {
        }
    }
}
