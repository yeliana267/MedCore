

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IUsersRepository : IBaseReporsitory<Users, int>
    {
        Task<Users> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task<OperationResult> ConfirmUserEmailAsync(int userId);
        Task<OperationResult> ResetPasswordAsync(int userId, string newPassword);
        Task<OperationResult> UpdateUserAsync(int id, Users entity);
        Task<OperationResult> DeleteUsersByIdAsync(int userId);
    }
}