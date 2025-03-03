using MedCore.Application.Base;
using MedCore.Application.Dtos.Medical.SpecialtiesDto;

namespace MedCore.Application.Interfaces.Medical
{
    public interface ISpecialtiesService : IBaseService<SaveSpecialtiesDto, UpdateSpecialtiesDto, RemoveSpecialtiesDto>
    {

    }
}
