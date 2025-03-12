
using MedCore.Application.Base;
using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;

namespace MedCore.Application.Interfaces.Insurance
{
    public interface IInsuranceProvidersService : IBaseService<SaveInsuranceProvidersDto, UpdateInsuranceProvidersDto, RemoveInsuranceProvidersDto>
    {

    }
}
