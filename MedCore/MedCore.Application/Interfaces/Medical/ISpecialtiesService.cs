using MedCore.Application.Base;
using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Domain.Base;

namespace MedCore.Application.Interfaces.Medical
{
    public interface ISpecialtiesService : IBaseService<SaveSpecialtiesDto, UpdateSpecialtiesDto, RemoveSpecialtiesDto>
    {
        Task<OperationResult> GetSpecialtyByNameAsync(string name);
    }
}
