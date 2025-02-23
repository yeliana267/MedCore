

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class InsuranceProvidersRepository : BaseRepository<InsuranceProviders, int> , IInsuranceProvidersRepository
    {
        private readonly MedCoreContext context;
        public InsuranceProvidersRepository(MedCoreContext context) : base(context) { 
            
            this.context = context;
        
        }
        public override Task<OperationResult> SaveEntityAsync(InsuranceProviders entity)
        {
            // validados


            return base.SaveEntityAsync(entity);
        }

        public override Task<OperationResult> UpdateEntityAsync(InsuranceProviders entity)
        {
            return base.UpdateEntityAsync(entity);
        }
    }
}
