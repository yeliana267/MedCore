using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface ISpecialtiesRepository : IBaseReporsitory<Specialties, short>
    {
        // Obtiene todas las especialidades que están activas en el sistema  
        Task<List<OperationResult>> GetActiveSpecialtiesAsync();

        // Obtiene una especialidad por su nombre  
        Task<OperationResult> GetSpecialtyByNameAsync(string name);

        // Elimina una especialidad por su ID
        Task<OperationResult> DeleteSpecialtyAsync(short id);
    }
}
