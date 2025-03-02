

namespace MedCore.Persistence.Interfaces.Insurance
{
    public interface IBaseInsuranceRepository : IDisposable 
    {
        IInsuranceProvidersRepository InsuranceProvidersRepository { get; }
        INetworkTypeRepository NetworkTypeRepository { get; }
        Task<int> CompleteAsync();
    }
}
