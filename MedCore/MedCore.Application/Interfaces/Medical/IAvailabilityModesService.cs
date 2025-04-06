using MedCore.Application.Base;
using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Domain.Base;

namespace MedCore.Application.Interfaces.Medical
{
    public interface IAvailabilityModesService : IBaseService<SaveAvailibilityModesDto, UpdateAvailibilityModesDto, RemoveAvailabilityModesDto>
    {

        // Obtiene un modo de disponibilidad por su nombre  
        Task<OperationResult> GetAvailabilityModeByNameAsync(string name);
    }
}
