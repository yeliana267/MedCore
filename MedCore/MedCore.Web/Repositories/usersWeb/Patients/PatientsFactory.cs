using MedCore.Application.Dtos.users.Patients;
using MedCore.Web.Models.users.Patients;

namespace MedCore.Web.Repositories.usersWeb.Patients
{
    public class PatientsFactory
    {
        public static SavePatientsDto CreateSaveDto(CreatePatientsModel model)
        => new()
        {
            UserID = model.UserID,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            EmergencyContactName = model.EmergencyContactName,
            EmergencyContactPhone = model.EmergencyContactPhone,
            BloodType = model.BloodType,
            Allergies = model.Allergies,
            InsuranceProviderID = model.InsuranceProviderID,
            CreatedAt = DateTime.UtcNow
        };

        public static UpdatePatientsDto CreateUpdateDto(EditPatientsModel model)
            => new()
            {
                PatientID = model.PatientID,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmergencyContactName = model.EmergencyContactName,
                EmergencyContactPhone = model.EmergencyContactPhone,
                BloodType = model.BloodType,
                Allergies = model.Allergies,
                InsuranceProviderID = model.InsuranceProviderID,
                UpdateAt = DateTime.UtcNow
            };
    }
}
