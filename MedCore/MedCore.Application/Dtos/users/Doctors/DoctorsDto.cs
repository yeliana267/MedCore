

namespace MedCore.Application.Dtos.users.Doctors
{
    public class DoctorsDto : DtoBase
    {
        public int SpecialtyID { get; set; }
        public int LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string ClinicAddress { get; set; }
        public int AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
