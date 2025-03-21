using MedCore.Application.Base;
using MedCore.Application.Dtos.users.Doctors;

namespace MedCore.Application.Interfaces.users
{
   public interface IDoctorsService : IBaseService<SaveDoctorsDto, UpdateDoctorsDto, RemoveDoctorsDto>
    {

    }
}
