using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface ISpecialtiesRepository : IBaseRepository<Specialties, short>
    {

        // Obtiene una especialidad por su nombre  
        Task<OperationResult> GetSpecialtyByNameAsync(string name);

        // Elimina una especialidad por su ID
        Task<OperationResult> DeleteSpecialtyAsync(short id);
    }
}
