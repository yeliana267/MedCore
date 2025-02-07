

namespace MedCore.Domain.Entities.users
{
    public sealed class UsersDoctors
    {
        public int DoctorID { get; set; }
        public short SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public int PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public short? AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
