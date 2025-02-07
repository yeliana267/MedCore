
namespace MedCore.Domain.Entities.users
{
    public sealed class UsersPatients
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public int EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
