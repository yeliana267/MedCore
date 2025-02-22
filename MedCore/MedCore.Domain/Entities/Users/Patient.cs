
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.Users
{
    public sealed class Patient : User
    {
        [Column("PatientID")]
        [Key]
        public int PatientID { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"[MFO]", ErrorMessage = "Género inválido (M, F, O)")]
        public char Gender { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmergencyContactName { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public int EmergencyContactPhone { get; set; }

        [Required]
        [MaxLength(2)]
        public string BloodType { get; set; }

        [Required]
        public string Allergies { get; set; }

        [Required]
        public int InsuranceProviderID { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
