using MedCore.Web.Models.Medical.MedicalRecordsModels;
using MedicalRecordsModel = MedCore.Model.Models.medical.MedicalRecordsModel;

namespace MedCore.Web.Interfaces.Medical
{
    public interface IMedicalRecordsWeb
    {
        Task<List<MedicalRecordsModel>> GetAllAsync();
        Task<MedicalRecordsModel> GetByIdAsync(int id);
        Task<EditMedicalRecordsModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateMedicalRecordsModel model);
        Task<bool> UpdateAsync(int id, EditMedicalRecordsModel model);
        Task<bool> DeleteAsync(int id);
    }
}
