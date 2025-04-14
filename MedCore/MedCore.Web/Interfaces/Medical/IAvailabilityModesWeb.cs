using MedCore.Web.Models.Medical.AvailabilityModes;
using AvailabilityModesModel = MedCore.Model.Models.medical.AvailabilityModesModel;

namespace MedCore.Web.Interfaces.Medical
{
    public interface IAvailabilityModesWeb
    {
        Task<List<AvailabilityModesModel>> GetAllAsync();
        Task<AvailabilityModesModel> GetByIdAsync(short id);
        Task<EditAvailabilityModesModel> GetEditModelByIdAsync(short id);
        Task<bool> CreateAsync(CreateAvailabilityModesModel model);
        Task<bool> UpdateAsync(short id, EditAvailabilityModesModel model);
        Task<bool> DeleteAsync(short id);
    }
}
