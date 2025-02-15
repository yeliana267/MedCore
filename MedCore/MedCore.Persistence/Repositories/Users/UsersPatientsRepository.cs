
using MedCore.Domain.Entities.Users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;

namespace MedCore.Persistence.Repositories.Users
{
    public class UsersPatientsRepository : BaseRepository<UsersPatients, int>, IUsersPatientsRepository
    {
        public UsersPatientsRepository(MedCoreContext context) : base(context) 
        { 
        }
    }
}
