
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.users;
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
        public async Task<OperationResult> GetByEmailAsync(int Id)
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
                    result.Message = $"No se encontró el usuario con ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: Usuario {id} no encontrado.");
                    return result;
                }

                _logger.LogInformation($"Actualizando usuario con ID {id}");

                // Actualizar los campos
                user.FirstName = entity.FirstName ?? user.FirstName;
                user.LastName = entity.LastName ?? user.LastName;
                user.Email = entity.Email ?? user.Email;
                user.RoleID = entity.RoleID ?? user.RoleID;
                user.Password = entity.Password ?? user.Password;

                _logger.LogInformation($"Antes de actualizar: {user.FirstName}, {user.Email}");

                // Guardar cambios en la BD
                await _context.SaveChangesAsync();

                _logger.LogInformation("Después de actualizar: Se guardaron los cambios correctamente.");

                // Retornar éxito
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



    }
    }
