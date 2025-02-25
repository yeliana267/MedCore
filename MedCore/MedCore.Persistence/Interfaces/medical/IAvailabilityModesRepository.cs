using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface IAvailabilityModesRepository : IBaseReporsitory<AvailabilityModes, short>
    {
        // Obtener modos actualizados en los últimos X días
        Task<List<OperationResult>> GetRecentlyUpdatedModesAsync(int days);

        // Obtiene un modo de disponibilidad por su nombre  
        Task<OperationResult> GetAvailabilityModeByNameAsync(string name);

        // Elimina un modo de disponibilidad por su ID
        Task<OperationResult> DeleteAvailabilityModeAsync(short id);
    }
}
