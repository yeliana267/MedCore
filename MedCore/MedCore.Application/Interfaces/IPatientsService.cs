

using MedCore.Application.Base;
using MedCore.Application.Dtos.users.Patients;

namespace MedCore.Application.Interfaces
{
   public interface IPatientsService : IBaseService<SavePatientsDto, UpdatePatientsDto, RemovePatientsDto>
    {
    }
}
