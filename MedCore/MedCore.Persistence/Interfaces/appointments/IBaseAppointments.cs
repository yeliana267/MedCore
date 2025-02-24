
namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IBaseAppointments : IDisposable
    {
         IAppointmentsRepository AppointmentsRepository
 { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository { get; }
        Task<int> CompleteAsync();
    }
}
