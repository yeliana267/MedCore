
using MedCore.Application.Dtos.Insurance.InsuranceProviders;

namespace MedCore.Application.Dtos.Insurance.InsuranceProvider
{
    public class SaveInsuranceProvidersDto : InsuranceProvidersDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
