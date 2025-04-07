using MedCore.Web.Models.Medical.SpecialtiesModels;
using SpecialtiesModel = MedCore.Model.Models.medical.SpecialtiesModel;

namespace MedCore.Web.Interfaces.Medical
{
    public interface ISpecialtiesWeb
    {
        Task<List<SpecialtiesModel>> GetAllAsync();
        Task<SpecialtiesModel> GetByIdAsync(short id);
        Task<EditSpecialtiesModel> GetEditModelByIdAsync(short id);
        Task<bool> CreateAsync(CreateSpecialtiesModel model);
        Task<bool> UpdateAsync(short id, EditSpecialtiesModel model);
        Task<bool> DeleteAsync(short id);
    }
}
