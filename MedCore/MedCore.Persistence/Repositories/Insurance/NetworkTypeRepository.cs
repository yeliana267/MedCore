

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class NetworkTypeRepository : BaseRepository<NetworkType, int>, INetworkTypeRepository
    {
        private readonly MedCoreContext context;
        public NetworkTypeRepository(MedCoreContext context) : base(context)
        {
            this.context = context;

        }

        public override Task<OperationResult> SaveEntityAsync(NetworkType entity)
        {

            /// validaciones
          


            return base.SaveEntityAsync(entity);
        }

        public override Task<OperationResult> UpdateEntityAsync(NetworkType entity)
        {
            return base.UpdateEntityAsync(entity);
        }
    }
}
