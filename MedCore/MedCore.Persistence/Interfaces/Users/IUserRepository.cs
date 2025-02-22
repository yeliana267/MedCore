
using MedCore.Domain.Repository;
using MedCore.Domain.Base;

namespace MedCore.Persistence.Interfaces.Users
{
    public interface IUserRepository : IBaseReporsitory<User, int>
    {
        //Validar que el correo electrónico no esté asociado a una cuenta existente.
        Task<User?> GetByEmailAsync(string email);

        //Confirmación de correo electrónico
        Task<User?> GetByEmailConfirmationTokenAsync(string token);

        //Recuperación de contraseña
        Task<User?> GetByPasswordResetTokenAsync(string token);

        //Método para guardar el token de confirmación
        Task<OperationResult> UpdateConfirmationTokenAsync(User user);

        //Método para guardar el token de reseteo de contraseña
        Task<OperationResult> UpdateResetPasswordTokenAsync(User user);

      

    }
}
