
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.Insurance;

namespace MedCore.Persistence.Test.Insurance
{
    public class UnitTestInsuranceProviders
    {
        public readonly IInsuranceProvidersRepository insuranceProvidersRepository;
        [Fact]
        public void SaveEntityAsync_ShouldReturnFailure_WhenInsuranceProvidersisNull()
        {
            InsuranceProviders insuranceProviders = null;

        }
    }
}
