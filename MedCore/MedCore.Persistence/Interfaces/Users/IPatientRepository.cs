

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Users;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.Users
{
    public interface IPatientRepository : IBaseReporsitory<Patient, int>
    {
        //Método para perosonalizar el perfil del paciente
        Task<OperationResult> UpdatePatientProfileAsync(Patient patient);

        //Método para actualizar la información médica del paciente
        Task<OperationResult> UpdateMedicalInfoAsync(Patient patient);
    }
}
