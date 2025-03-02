namespace MedCore.Persistence.Interfaces.medical
{
    public interface IBaseMedical : IDisposable
    {
        IAvailabilityModesRepository AvailabilityModesRepository { get; }
        IMedicalRecordsRepository MedicalRecordsRepository { get; }
        ISpecialtiesRepository SpecialtiesRepository { get; }
        Task<int> CompleteAsync();
    }
}
