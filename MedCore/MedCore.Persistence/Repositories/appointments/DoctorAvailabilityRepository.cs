

using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;

namespace MedCore.Persistence.Repositories.appointments
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability, int>, IDoctorAvailabilityRepository
    {
        public DoctorAvailabilityRepository(MedCoreContext context) : base(context)
        {
        }
    }
}
