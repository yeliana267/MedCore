
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.Insurance;

namespace MedCore.Persistence.Test.Insurance
{
    public class UnitTestInsuranceProviders
    {
        public readonly IInsuranceProvidersRepository insuranceProvidersRepository;


        public UnitTestInsuranceProviders()
        {
            
        }

        [Fact]
        public void SaveEntityAsync_ShouldReturnFailure_WhenInsuranceProvidersisNull()
        {
            // Arrange
            InsuranceProviders insuranceProviders = null;

            // Act

            // Assert

        }
    }
}
