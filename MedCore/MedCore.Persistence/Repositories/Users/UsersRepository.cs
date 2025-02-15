

using MedCore.Domain.Entities.Users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;

namespace MedCore.Persistence.Repositories.Users
{
    public class UsersRepository : BaseRepository<Users, int>, IUsersRepository
    {
        public UsersRepository(MedCoreContext context) : base(context)
        {
        }
    }
}
