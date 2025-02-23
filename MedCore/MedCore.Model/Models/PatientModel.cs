

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models
{
    public class PatientModel
    {
        public int PatientID { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public char Gender { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string EmergencyContactName { get; set; }

        public int EmergencyContactPhone { get; set; }

        [MaxLength(2)]
        public string BloodType { get; set; }

        [MaxLength(1000)]
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
        public int UserId { get; set; }
    }
}
