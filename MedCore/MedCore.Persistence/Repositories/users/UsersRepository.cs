

using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.users
{
    public class UsersRepository : BaseRepository<Users, int>, IUsersRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<UsersRepository> _logger;
        private readonly IConfiguration _configuration;
        public UsersRepository(MedCoreContext context, ILogger<UsersRepository> loger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = loger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ConfirmUserEmailAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> DeleteUsersByIdAsync(int userId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM [MedicalAppointment].[users].[Users] WHERE UserID = {0}", userId);

                if (querys > 0)
                {
                    result.Success = true;
                    result.Message = "User eliminado exitosamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontró ningún user con ese ID.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el user.";
                _logger.LogError($"Error al eliminar el user con ID {userId}: {ex.Message}", ex);
            }
            return result;

        }
        public async Task<Users> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> ResetPasswordAsync(int userId, string newPassword)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> UpdateUserAsync(int id, Users entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró el usuario con ID.";
                    _logger.LogWarning($"Intento de actualización fallido: Usuario no encontrado.");
                    return result;
                }

                _logger.LogInformation($"Actualizando usuario con ID");

                
                user.FirstName = entity.FirstName ?? user.FirstName;
                user.LastName = entity.LastName ?? user.LastName;
                user.Email = entity.Email ?? user.Email;
                user.RoleID = entity.RoleID ?? user.RoleID;
                user.Password = entity.Password ?? user.Password;

                _logger.LogInformation($"Antes de actualizar: {user.FirstName}, {user.Email}");

                
                await _context.SaveChangesAsync();

                _logger.LogInformation("Después de actualizar: Se guardaron los cambios correctamente.");

                
                result.Success = true;
                result.Message = $"Usuario con ID {id} actualizado correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el usuario: {ex.Message}";
                _logger.LogError($"Error en UpdateUserAsync para el usuario {id}: {ex.Message}", ex);
            }

            return result;
        }
        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }

}
