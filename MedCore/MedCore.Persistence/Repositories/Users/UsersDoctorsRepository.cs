

using MedCore.Domain.Entities.Users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;

namespace MedCore.Persistence.Repositories.Users
{
    public class UsersDoctorsRepository : BaseRepository<UsersDoctors, int>, IUsersDoctorsRepository
    {
        public UsersDoctorsRepository(MedCoreContext context) : base(context)
        {

        }
    }
}
