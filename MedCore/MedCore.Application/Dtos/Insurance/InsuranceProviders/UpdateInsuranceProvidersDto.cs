
namespace MedCore.Application.Dtos.Insurance.InsuranceProviders
{
    public class UpdateInsuranceProvidersDto : InsuranceProvidersDto
    {
        public int InsuranceProviderID { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
