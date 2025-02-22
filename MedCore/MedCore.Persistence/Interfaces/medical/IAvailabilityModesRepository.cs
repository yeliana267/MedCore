using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface IAvailabilityModesRepository : IBaseReporsitory<AvailabilityModes, short>
    {
        public Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity);

    }
}
