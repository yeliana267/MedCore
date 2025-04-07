using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface IAvailabilityModesRepository : IBaseRepository<AvailabilityModes, short>
    {
        //obtener disponibilidad por id 
        Task<OperationResult> GetAvailabilityModeByIdAsync(short id);

        // Obtiene un modo de disponibilidad por su nombre  
        Task<OperationResult> GetAvailabilityModeByNameAsync(string name);
    }
}
