using MedCore.Model.Models.users;
using MedCore.Web.Models.users.users;

namespace MedCore.Web.Interfaces.users
{
    public interface IUsersWeb
    {
        Task<List<UsersModel>> GetAllAsync();
        Task<UsersModel> GetByIdAsync(int id);
        Task<EditUsersModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateUsersModel model);
        Task<bool> UpdateAsync(int id, EditUsersModel model);
        Task<bool> DeleteAsync(int id);
    }
}
