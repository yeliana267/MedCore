
using MedCore.Domain.Entities.system;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.System;

namespace MedCore.Persistence.Repositories.System
{
    public class RolesRepository : BaseRepository<Roles, int>, IRolesRepository
    {
        public RolesRepository(MedCoreContext context) : base(context) 
        {
        }
    }
}
