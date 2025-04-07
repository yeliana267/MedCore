

using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.users
{
    public class PatientsModel
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(1)]
        public char Gender { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        public string EmergencyContactName { get; set; }
        [Required]
        public string EmergencyContactPhone { get; set; }
        [Required]
        [MaxLength(2)]
        public string BloodType { get; set; }
        [Required]
        public string Allergies { get; set; }
        [Required]
        public int InsuranceProviderID { get; set; }
    }
}
