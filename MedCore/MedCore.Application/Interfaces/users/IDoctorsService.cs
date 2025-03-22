using MedCore.Application.Base;
using MedCore.Application.Dtos.users.Doctors;
using MedCore.Domain.Base;

namespace MedCore.Application.Interfaces.users
{
   public interface IDoctorsService : IBaseService<SaveDoctorsDto, UpdateDoctorsDto, RemoveDoctorsDto>
    {
        Task<OperationResult> UpdateConsultationFeeAsync(int doctorId, decimal consultationFee);
        Task<OperationResult> ActivateDoctorAsync(int doctorId);
        Task<OperationResult> DeactivateDoctorAsync(int doctorId);
    }
}
