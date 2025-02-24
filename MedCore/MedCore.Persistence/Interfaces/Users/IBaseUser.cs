using MedCore.Persistence.Interfaces.appointments;

namespace MedCore.Persistence.Interfaces.Users
{
    public interface IBaseUsers : IDisposable
    {
        IUserRepository UserRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        Task<int> CompleteAsync();

    }
}
