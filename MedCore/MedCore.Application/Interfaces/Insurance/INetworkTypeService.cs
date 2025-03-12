
using MedCore.Application.Base;
using MedCore.Application.Dtos.Insurance.NetworkType;

namespace MedCore.Application.Interfaces.Insurance
{
    public interface INetworkTypeService : IBaseService<SaveNetworkTypeDto, UpdateNetworkTypeDto, RemoveNetwokTypeDto>
    {
    }
}
