using MedCore.Web.Models.Insurance.InsuranceProviders;

namespace MedCore.Web.Interfaces.Insurance
{
    public interface IInsuranceProvidersWeb
    {
        Task<List<InsuranceProvidersModel>> GetAllAsync();
        Task<InsuranceProvidersModel> GetByIdAsync(int id);
        Task<EditInsuranceProvidersModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateInsuranceProvidersModel model);
        Task<bool> UpdateAsync(int id, EditInsuranceProvidersModel model);
        Task<bool> DeleteAsync(int id);
    }
}
