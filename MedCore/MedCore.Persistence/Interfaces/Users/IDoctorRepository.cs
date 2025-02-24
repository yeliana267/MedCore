

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Users;

namespace MedCore.Persistence.Interfaces.Users
{
    public interface IDoctorRepository
    {
        //Método para personalizar el perfil del doctor
        Task<OperationResult> UpdateDoctorProfileAsync(Doctor doctor);

        //Método para actualizar la información médica del doctor
        Task<OperationResult> UpdateDoctorInfoAsync(Doctor doctor);

        //Método para obtener los doctores por especialidad
        //Task<List<Doctor>> GetDoctorsBySpecialtyAsync(short specialtyId);
    }
}
