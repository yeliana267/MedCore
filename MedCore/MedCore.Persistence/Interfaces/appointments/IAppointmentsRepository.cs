

using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Repository;
using System.Buffers;
using System.Net.NetworkInformation;

namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IAppointmentsRepository : IBaseReporsitory<Appointments, int> 
    {

    }
}
