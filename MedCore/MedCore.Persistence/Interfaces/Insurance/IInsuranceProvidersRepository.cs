
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.Insurance
{
    public interface IInsuranceProvidersRepository : IBaseRepository<InsuranceProviders, int>
    {
        // Count of providers by insurance network types id
        Task<OperationResult> CountInsuranceProvidersByNetworkTypeId(int id);

        
    

    }
}
