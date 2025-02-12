
using MedCore.Domain.Entities.system;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.System;

namespace MedCore.Persistence.Repositories.System
{
    public class StatusRepository : BaseRepository<Status, int>, IStatusRepository
    {
        public StatusRepository(MedCoreContext context) : base(context) 
        { 
        }
    }
}
