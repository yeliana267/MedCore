

using MedCore.Domain.Entities.users;

namespace MedCore.Application.Dtos.users.Doctors
{
    public class UpdateDoctorsDto : DoctorsDto
    {
        public int DoctorID { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? LicenseNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public short? AvailabilityModeId { get; set; }
        public DateOnly? LicenseExpirationDate { get; set; }
        public bool? IsActive { get; set; }
        public short? SpecialtyID { get; set; }

    }
}
