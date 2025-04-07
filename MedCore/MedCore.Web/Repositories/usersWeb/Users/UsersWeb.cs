using MedCore.Application.Dtos.users.Users;
using MedCore.Domain.Base;
using MedCore.Model.Models.users;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.users;
using MedCore.Web.Models.users.users;

namespace MedCore.Web.Repositories.usersWeb.Users
{
    public class UsersWeb : IUsersWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<UsersWeb> _logger;

        public UsersWeb(IApiClient apiClient, ILogger<UsersWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<UsersModel>> GetAllAsync()
        {
            try
            {
                var users = await _apiClient.GetAsync<List<UsersModel>>("Users/GetUsers");
                return users ?? new List<UsersModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                throw;
            }
        }

        public async Task<UsersModel> GetByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<UsersModel>($"Users/GetUsersByID?Id={id}")
                    ?? new UsersModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario con ID {id}");
                throw;
            }
        }

        public async Task<EditUsersModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditUsersModel>($"Users/GetUsersByID?Id={id}")
                    ?? new EditUsersModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para usuario con ID {id}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateUsersModel model)
        {
            try
            {
                var dto = UsersFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveUsersDto, OperationResult>(
                    "Users/SaveUsers", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditUsersModel model)
        {
            try
            {
                var dto = UsersFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateUsersDto, OperationResult>(
                    $"Users/UpdateUsers/{id}", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar usuario con ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemoveUsersDto { UserID = id };
                var result = await _apiClient.DeleteAsync<RemoveUsersDto, OperationResult>(
                    "Users/DeleteUsers", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar usuario con ID {id}");
                throw;
            }
        }
    }
}

