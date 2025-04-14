using MedCore.Web.Models.users.Patients;

namespace MedCore.Web.Interfaces.users
{
    public interface IPatientsWeb
    {
        Task<List<PatientsModel>> GetAllAsync();
        Task<PatientsModel> GetByIdAsync(int id);
        Task<EditPatientsModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreatePatientsModel model);
        Task<bool> UpdateAsync(int id, EditPatientsModel model);
        Task<bool> DeleteAsync(int id);
    }
}
