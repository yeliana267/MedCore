using MedCore.Application.Base;
using MedCore.Application.Dtos.users.Users;

namespace MedCore.Application.Interfaces.users
{
    public interface IUsersService : IBaseService<SaveUsersDto, UpdateUsersDto, RemoveUsersDto>
    {

    }
}
