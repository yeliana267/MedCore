using MedCore.Web.Models.Insurance.NetworkType;

namespace MedCore.Web.Interfaces.Insurance
{
    public interface INetworkTypeWeb
    {
        Task<List<NetworkTypeModel>> GetAllAsync();
        Task<NetworkTypeModel> GetByIdAsync(int id);
        Task<EditNetworkTypeModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateNetworkTypeModel model);
        Task<bool> UpdateAsync(int id, EditNetworkTypeModel model);
        Task<bool> DeleteAsync(int id);
    }
}
