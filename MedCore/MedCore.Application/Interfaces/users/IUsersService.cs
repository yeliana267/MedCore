using MedCore.Application.Base;
using MedCore.Application.Dtos.users.Users;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;

namespace MedCore.Application.Interfaces.users
{
    public interface IUsersService : IBaseService<SaveUsersDto, UpdateUsersDto, RemoveUsersDto>
    {
        Task<OperationResult> GetUserByEmailAsync(string email);
        Task<OperationResult> ActivateUserAsync(int userId);
        Task<OperationResult> DeactivateUserAsync(int userId);

    }
}
