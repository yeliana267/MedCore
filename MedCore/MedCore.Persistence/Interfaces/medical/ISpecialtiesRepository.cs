using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface ISpecialtiesRepository : IBaseReporsitory<Specialties, short>
    {
        Task<OperationResult> UpdateEntityAsync(Specialties entity);
    }
}
