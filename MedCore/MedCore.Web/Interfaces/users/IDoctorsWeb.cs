using MedCore.Web.Models.users.Doctors;

namespace MedCore.Web.Interfaces.users
{
    public interface IDoctorsWeb
    {
        Task<List<DoctorsModel>> GetAllAsync();
        Task<DoctorsModel> GetByIdAsync(int id);
        Task<EditDoctorsModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateDoctorsModel model);
        Task<bool> UpdateAsync(int id, EditDoctorsModel model);
        Task<bool> DeleteAsync(int id);
    }
}
