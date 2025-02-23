
using System.Collections.Generic;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.Insurance
{
    public interface INetworkTypeRepository : IBaseReporsitory <NetworkType, int>
    {
        //Obtener un tipo de red por ID
        Task<OperationResult> GetNetworkTypeById(int id);

        //Listar todos los tipos de red disponibles
        Task<OperationResult> GetNetworkTypeList ();
    }
}
