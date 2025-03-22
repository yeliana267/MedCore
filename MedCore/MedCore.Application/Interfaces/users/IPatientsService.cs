using MedCore.Application.Base;
using MedCore.Application.Dtos.users.Patients;
using MedCore.Domain.Base;

namespace MedCore.Application.Interfaces.users
{
   public interface IPatientsService : IBaseService<SavePatientsDto, UpdatePatientsDto, RemovePatientsDto>
    {
        Task<OperationResult> UpdateEmergencyContactAsync(int patientId, string contactName, string contactPhone);
        Task<OperationResult> DeactivatePatientAsync(int patientId);
        Task<OperationResult> ActivatePatientAsync(int patientId);

    }
}
