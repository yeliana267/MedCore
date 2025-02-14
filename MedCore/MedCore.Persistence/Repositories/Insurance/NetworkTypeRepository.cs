

using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class NetworkTypeRepository : BaseRepository<NetworkType, int>, INetworkTypeRepository
    {
        public NetworkTypeRepository(MedCoreContext context) : base(context)
        {

        }
    }
}
