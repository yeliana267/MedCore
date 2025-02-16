
    using MedCore.Domain.Base;
    using MedCore.Domain.Entities.Users;
    using MedCore.Persistence.Base;
    using MedCore.Persistence.Context;
    using MedCore.Persistence.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Persistence.Repositories.Users
{

    public class UsersDoctorsRepository : BaseRepository<UsersDoctors, int>, IUsersDoctorsRepository
    {
        private readonly MedCoreContext context;
        public UsersDoctorsRepository(MedCoreContext context) : base(context)
        {
            this.context = context;
        }
        public override Task<OperationResult> SaveEntityAsync(UsersDoctors entity)
        {
           

            return base.SaveEntityAsync(entity);
        }
    }
}