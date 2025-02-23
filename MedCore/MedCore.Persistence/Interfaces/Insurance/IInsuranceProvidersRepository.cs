
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.Insurance
{
    public interface IInsuranceProvidersRepository : IBaseReporsitory<InsuranceProviders, int>
    {
        // Count of providers by insurance network types
        Task<OperationResult> CountInsuranceProvidersByNetworkTypeId(int id);
    }
}
