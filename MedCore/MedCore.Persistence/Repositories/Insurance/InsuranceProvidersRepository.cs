

using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class InsuranceProvidersRepository : BaseRepository<InsuranceProviders, int> , IInsuranceProvidersRepository
    {
        public InsuranceProvidersRepository(MedCoreContext context) : base(context) { 
        
        }
    }
}
