using MedCore.Application.Dtos.users.Doctors;
using MedCore.Web.Models.users.Doctors;

namespace MedCore.Web.Repositories.usersWeb.Doctors
{
    public class DoctorsFactory
    {
        public static SaveDoctorsDto CreateSaveDto(CreateDoctorsModel model)
        => new()
        {
            UserID = model.UserID,
            SpecialtyID = model.SpecialtyID,
            LicenseNumber = model.LicenseNumber,
            PhoneNumber = model.PhoneNumber,
            YearsOfExperience = model.YearsOfExperience,
            Education = model.Education,
            Bio = model.Bio,
            ConsultationFee = model.ConsultationFee,
            ClinicAddress = model.ClinicAddress,
            AvailabilityModeId = model.AvailabilityModeId,
            LicenseExpirationDate = model.LicenseExpirationDate,
            IsActive = model.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        public static UpdateDoctorsDto CreateUpdateDto(EditDoctorsModel model)
            => new()
            {
                DoctorID = model.DoctorID,
                SpecialtyID = model.SpecialtyID,
                LicenseNumber = model.LicenseNumber,
                PhoneNumber = model.PhoneNumber,
                YearsOfExperience = model.YearsOfExperience,
                Education = model.Education,
                Bio = model.Bio,
                ConsultationFee = model.ConsultationFee,
                ClinicAddress = model.ClinicAddress,
                AvailabilityModeId = model.AvailabilityModeId,
                LicenseExpirationDate = model.LicenseExpirationDate,
                IsActive = model.IsActive,
                UpdatedAt = DateTime.UtcNow
            };
    }
}
