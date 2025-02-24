
using MedCore.Domain.Repository;
using MedCore.Domain.Base;

namespace MedCore.Persistence.Interfaces.Users
{
    public interface IUserRepository : IBaseReporsitory<User, int>
    {
        //Método para obtener un usuario por email
        Task<User?> GetByEmailAsync(string email);

        //Confirmación de correo electrónico
        Task<User?> GetByEmailConfirmationTokenAsync(string token);

        //Método para obtener un usuario por token de reseteo de contraseña
        Task<User?> GetByPasswordResetTokenAsync(string token);

        //Método para guardar el token de confirmación
        Task<OperationResult> UpdateConfirmationTokenAsync(User user);

        //Método para guardar el token de reseteo de contraseña
        Task<OperationResult> UpdateResetPasswordTokenAsync(User user);

        //Método para actualizar la información de contacto
        Task<OperationResult> UpdateContactInfoAsync(User user);

    }
}
