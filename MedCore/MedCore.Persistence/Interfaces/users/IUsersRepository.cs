

using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IUsersRepository : IBaseReporsitory<Users, int>
    {
        Task<OperationResult> GetByEmailAsync(int Id);
    }
}
