

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IUsersRepository : IBaseReporsitory<Users, int>
    {
        Task<OperationResult> GetByEmailAsync(string email); // Obtener un usuario por email
        Task<OperationResult> GetUsersByRoleAsync(int roleId); // Obtener usuarios por rol
        Task<OperationResult> DeactivateUserAsync(int userId); // Desactivar un usuario
        Task<OperationResult> ActivateUserAsync(int userId); // Activar un usuario
    }
}