

using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;

namespace MedCore.Persistence.Repositories.appointments
{
    public class AppointmentsRepository : BaseRepository<Appointments, int>, IAppointmentsRepository
    {
        public AppointmentsRepository(MedCoreContext context) : base(context)
        {
        }
    }

}
