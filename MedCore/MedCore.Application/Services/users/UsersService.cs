using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MedCore.Application.Dtos.users.Users;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;
        private readonly IConfiguration _configuration;

        public UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ActivateUserAsync(int userId)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                if (userId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del usuario no es válido.";
                    _logger.LogWarning("Intento de activación fallido: ID de usuario no válido.");
                    return result;
                }

                
                var userResult = await _usersRepository.GetEntityByIdAsync(userId);
                if (userResult == null)
                {
                    result.Success = false;
                    result.Message = "Usuario no encontrado.";
                    _logger.LogWarning($"Intento de activación fallido: Usuario con ID {userId} no encontrado.");
                    return result;
                }

                
                if (userResult.IsActive) 
                {
                    result.Success = false;
                    result.Message = "El usuario ya está activo.";
                    _logger.LogWarning($"Intento de activación fallido: Usuario con ID {userId} ya está activo.");
                    return result;
                }

                
                var activationResult = await _usersRepository.ActivateUserAsync(userId);
                if (activationResult.Success)
                {
                    result.Success = true;
                    result.Message = "Usuario activado correctamente.";
                    result.Data = activationResult.Data; 
                }
                else
                {
                    result.Success = false;
                    result.Message = activationResult.Message;
                    _logger.LogWarning($"Error al activar el usuario con ID {userId}: {activationResult.Message}");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al activar el usuario con ID {userId}: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> DeactivateUserAsync(int userId)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                if (userId <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del usuario no es válido.";
                    _logger.LogWarning("Intento de desactivación fallido: ID de usuario no válido.");
                    return result;
                }

                
                var userResult = await _usersRepository.GetEntityByIdAsync(userId);
                if (userResult == null)
                {
                    result.Success = false;
                    result.Message = "Usuario no encontrado.";
                    _logger.LogWarning($"Intento de desactivación fallido: Usuario con ID {userId} no encontrado.");
                    return result;
                }

                
                if (!userResult.IsActive) 
                {
                    result.Success = false;
                    result.Message = "El usuario ya está desactivado.";
                    _logger.LogWarning($"Intento de desactivación fallido: Usuario con ID {userId} ya está desactivado.");
                    return result;
                }

                
                var deactivationResult = await _usersRepository.DeactivateUserAsync(userId);
                if (deactivationResult.Success)
                {
                    result.Success = true;
                    result.Message = "Usuario desactivado correctamente.";
                    result.Data = deactivationResult.Data; 
                }
                else
                {
                    result.Success = false;
                    result.Message = deactivationResult.Message;
                    _logger.LogWarning($"Error al desactivar el usuario con ID {userId}: {deactivationResult.Message}");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al desactivar el usuario con ID {userId}: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var users = await _usersRepository.GetAllAsync();

                
                if (users == null || !users.Any())
                {
                    result.Success = false;
                    result.Message = "No se encontraron usuarios.";
                    _logger.LogWarning("No se encontraron usuarios en la base de datos.");
                }
                else
                {
                    result.Data = users;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al obtener todos los usuarios: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
               
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del usuario no es válido.";
                    _logger.LogWarning("Intento de obtener usuario fallido: ID no válido.");
                    return result;
                }

                var user = await _usersRepository.GetEntityByIdAsync(id);

                
                if (user == null)
                {
                    result.Success = false;
                    result.Message = "Usuario no encontrado.";
                    _logger.LogWarning($"Usuario con ID {id} no encontrado.");
                }
                else
                {
                    result.Data = user;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al obtener el usuario con ID {id}: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> GetUserByEmailAsync(string email)
        {
            OperationResult result = new OperationResult();
            try
            {
               
                if (string.IsNullOrWhiteSpace(email))
                {
                    result.Success = false;
                    result.Message = "El correo electrónico no puede estar vacío.";
                    _logger.LogWarning("Intento de obtener usuario fallido: Correo electrónico vacío.");
                    return result;
                }

                
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; 
                if (!Regex.IsMatch(email, emailPattern))
                {
                    result.Success = false;
                    result.Message = "El formato del correo electrónico no es válido.";
                    _logger.LogWarning("Intento de obtener usuario fallido: Formato de correo electrónico no válido.");
                    return result;
                }

                
                var userResult = await _usersRepository.GetByEmailAsync(email);
                if (userResult == null)
                {
                    result.Success = false;
                    result.Message = "Usuario no encontrado.";
                    _logger.LogWarning($"Intento de obtener usuario fallido: Usuario con correo electrónico {email} no encontrado.");
                }
                else
                {
                    result.Success = true;
                    result.Message = "Usuario encontrado correctamente.";
                    result.Data = userResult;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al obtener el usuario con correo electrónico {email}: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> Remove(RemoveUsersDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
               
                if (dto.UserID <= 0)
                {
                    result.Success = false;
                    result.Message = "ID de usuario no válido.";
                    _logger.LogWarning("Intento de eliminación fallido: ID de usuario no válido.");
                    return result;
                }

                
                var deleteResult = await _usersRepository.DeleteEntityByIdAsync(dto.UserID);
                if (deleteResult.Success)
                {
                    result.Success = true;
                    result.Message = "Usuario eliminado correctamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = deleteResult.Message;
                    _logger.LogWarning($"Error al eliminar el usuario con ID {dto.UserID}: {deleteResult.Message}");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al eliminar el usuario con ID {dto.UserID}: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> Save(SaveUsersDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                if (string.IsNullOrWhiteSpace(dto.FirstName))
                {
                    result.Success = false;
                    result.Message = "El nombre del usuario no puede estar vacío.";
                    _logger.LogWarning("Intento de guardado fallido: Nombre de usuario vacío.");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(dto.LastName))
                {
                    result.Success = false;
                    result.Message = "El apellido del usuario no puede estar vacío.";
                    _logger.LogWarning("Intento de guardado fallido: Apellido de usuario vacío.");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    result.Success = false;
                    result.Message = "El correo electrónico del usuario no puede estar vacío.";
                    _logger.LogWarning("Intento de guardado fallido: Correo electrónico de usuario vacío.");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    result.Success = false;
                    result.Message = "La contraseña del usuario no puede estar vacía.";
                    _logger.LogWarning("Intento de guardado fallido: Contraseña de usuario vacía.");
                    return result;
                }

                if (dto.RoleID <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID del rol no es válido.";
                    _logger.LogWarning("Intento de guardado fallido: ID de rol no válido.");
                    return result;
                }

                
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(dto.Email, emailPattern))
                {
                    result.Success = false;
                    result.Message = "El formato del correo electrónico no es válido.";
                    _logger.LogWarning("Intento de guardado fallido: Formato de correo electrónico no válido.");
                    return result;
                }

                
                var user = new Users
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = dto.Password,
                    RoleID = dto.RoleID,
                    IsActive = dto.IsActive,
                    CreatedAt = dto.CreatedAt
                };

                
                var saveResult = await _usersRepository.SaveEntityAsync(user);
                if (saveResult.Success)
                {
                    result.Success = true;
                    result.Message = "Usuario guardado correctamente.";
                    result.Data = user;
                }
                else
                {
                    result.Success = false;
                    result.Message = saveResult.Message;
                    _logger.LogWarning($"Error al guardar el usuario: {saveResult.Message}");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                _logger.LogError($"Error al guardar el usuario: {ex.Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> Update(UpdateUsersDto dto)
        {
            var result = new OperationResult();

            if (dto == null || dto.UserID <= 0)
            {
                result.Success = false;
                result.Message = "Datos inválidos para la actualización.";
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {

                var existing = await _usersRepository.GetEntityByIdAsync(dto.UserID);
                if (existing == null)
                {
                    result.Success = false;
                    result.Message = "Usuario no encontrado.";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                
                if (!existing.IsActive && dto.IsActive.HasValue && !dto.IsActive.Value)
                {
                    result.Success = false;
                    result.Message = "No se pueden modificar usuarios inactivos.";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                
                if (dto.Email != null && !new EmailAddressAttribute().IsValid(dto.Email))
                {
                    result.Success = false;
                    result.Message = "El formato del email no es válido.";
                    _logger.LogWarning(result.Message);
                    return result;
                }



                
                if (dto.FirstName != null)
                {
                    _logger.LogDebug("Actualizando FirstName de '{Old}' a '{New}'", existing.FirstName, dto.FirstName);
                    existing.FirstName = dto.FirstName;
                }

                if (dto.LastName != null)
                {
                    _logger.LogDebug("Actualizando LastName de '{Old}' a '{New}'", existing.LastName, dto.LastName);
                    existing.LastName = dto.LastName;
                }

                if (dto.Email != null)
                {
                    _logger.LogDebug("Actualizando Email de '{Old}' a '{New}'", existing.Email, dto.Email);
                    existing.Email = dto.Email;
                }

                if (dto.RoleID.HasValue)
                {
                    _logger.LogDebug("Actualizando RoleID de {Old} a {New}", existing.RoleID, dto.RoleID);
                    existing.RoleID = dto.RoleID.Value;
                }

                if (dto.IsActive.HasValue)
                {
                    _logger.LogDebug("Actualizando IsActive de {Old} a {New}", existing.IsActive, dto.IsActive);
                    existing.IsActive = dto.IsActive.Value;
                }


                
                var updateResult = await _usersRepository.UpdateEntityAsync(dto.UserID, existing);
                if (!updateResult.Success)
                {
                    _logger.LogError("Error al guardar en repositorio: {Message}", updateResult.Message);
                    return updateResult;
                }

                result.Success = true;
                result.Message = "Usuario actualizado correctamente.";
                result.Data = existing;
                _logger.LogInformation(result.Message);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar usuario: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }

            return result;
        }
    }
}