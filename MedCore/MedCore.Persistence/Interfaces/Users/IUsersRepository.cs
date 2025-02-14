
using MedCore.Domain.Entities.Users;
using MedCore.Domain.Repository;
using Microsoft.Identity.Client;


namespace MedCore.Persistence.Interfaces.Users
{
    public interface IUsersRepository : IBaseReporsitory<IUsersRepository, int>
    {
    }
}
