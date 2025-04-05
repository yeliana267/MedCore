namespace MedCore.Web.Models.users.Doctors
{
    public class CreateDoctorsModel
    {
        public short SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string ClinicAddress { get; set; }
        public short? AvailabilityModeId { get; set; }
        public DateOnly LicenseExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
