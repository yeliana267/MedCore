

using System.Text.RegularExpressions;
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

        public async Task<OperationResult> ActivateUserAsync(int userId)
        {
            try
            {
  
                if (userId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "user ID no valido."
                    };
                }

                // Buscar el usuario en la base de datos
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "User no existe."
                    };
                }

                // Activar el usuario
                user.IsActive = true;
                user.UpdatedAt = DateTime.UtcNow; // Actualizar la fecha de modificación
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "User Activado.",
                    Data = user // Opcional: devolver el usuario actualizado
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error activando user: {ex.Message}"
                };
            }
        }

        public async Task<OperationResult> DeactivateUserAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "user ID no valido."
                    };
                }

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "User no encontrado."
                    };
                }


                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow; 
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "User desactivado.",
                    Data = user 
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error desactivando user: {ex.Message}"
                };
            }
        }

        public override async Task<OperationResult> DeleteEntityByIdAsync(int userId)
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

        public async Task<OperationResult> GetByEmailAsync(string email)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(email))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Email no puede ser nulo."
                    };
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "User no encontrado."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "User encontrado.",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error obteniendo user: {ex.Message}"
                };
            }
        }

        public async Task<OperationResult> GetUsersByRoleAsync(int roleId)
        {
            try
            {
                if (roleId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Role ID invalido."
                    };
                }

                var users = await _context.Users
                    .Where(u => u.RoleID == roleId)
                    .ToListAsync();

                if (users == null || !users.Any())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "No users enontrados por el rol."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "Usuario encontrado.",
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error buscando users: {ex.Message}"
                };
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(int id, Users entity)
        {

            OperationResult result = new OperationResult();

            try
            {
                // Validar que el ID sea válido
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del usuario no es válido.";
                    _logger.LogWarning("Intento de actualización fallido: ID de usuario no válido.");
                    return result;
                }

                // Validar que la entidad no sea nula
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad de usuario no puede ser nula.";
                    _logger.LogWarning("Intento de actualización fallido: Entidad de usuario nula.");
                    return result;
                }

                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(entity.FirstName))
                {
                    result.Success = false;
                    result.Message = "El nombre del usuario no puede estar vacío.";
                    _logger.LogWarning("Intento de actualización fallido: Nombre de usuario vacío.");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(entity.LastName))
                {
                    result.Success = false;
                    result.Message = "El apellido del usuario no puede estar vacío.";
                    _logger.LogWarning("Intento de actualización fallido: Apellido de usuario vacío.");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(entity.Email))
                {
                    result.Success = false;
                    result.Message = "El correo electrónico del usuario no puede estar vacío.";
                    _logger.LogWarning("Intento de actualización fallido: Correo electrónico de usuario vacío.");
                    return result;
                }

                if (entity.RoleID == null || entity.RoleID <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del rol no es válido.";
                    _logger.LogWarning("Intento de actualización fallido: ID de rol no válido.");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(entity.Password))
                {
                    result.Success = false;
                    result.Message = "La contraseña del usuario no puede estar vacía.";
                    _logger.LogWarning("Intento de actualización fallido: Contraseña de usuario vacía.");
                    return result;
                }

                // Validar formato del correo electrónico 
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; 
                if (!Regex.IsMatch(entity.Email, emailPattern))
                {
                    result.Success = false;
                    result.Message = "El formato del correo electrónico no es válido.";
                    _logger.LogWarning("Intento de actualización fallido: Formato de correo electrónico no válido.");
                    return result;
                }

                // Buscar el usuario en la base de datos
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró el usuario con ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: Usuario no encontrado.");
                    return result;
                }

                _logger.LogInformation($"Actualizando usuario con ID {id}");

                // Actualizar los campos del usuario
                user.FirstName = entity.FirstName ?? user.FirstName;
                user.LastName = entity.LastName ?? user.LastName;
                user.Email = entity.Email ?? user.Email;
                user.RoleID = entity.RoleID ?? user.RoleID;
                user.Password = entity.Password ?? user.Password;

                _logger.LogInformation($"Antes de actualizar: {user.FirstName}, {user.Email}");

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation("Después de actualizar: Se guardaron los cambios correctamente.");

                // Devolver resultado exitoso
                result.Success = true;
                result.Message = $"Usuario con ID {id} actualizado correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el usuario: {ex.Message}";
                _logger.LogError($"Error en UpdateEntityAsync para el usuario {id}: {ex.Message}", ex);
            }

            return result;
        }
    }

}
