

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.Users
{
    public sealed class Doctor : User
    {
        [Column("DoctorID")]
        [Key]
        public int IdDoctorID { get; set; }
        public short SpecialtyID { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        public int YearsOfExperience { get; set; }

        [Required]
        public string Education { get; set; }
        public string? Bio { get; set; }

        [Range(0, 9999999.99)]
        public decimal? ConsultationFee { get; set; }

        [MaxLength(255)]
        public string? ClinicAddress { get; set; }
        public short? AvailabilityModeId { get; set; }

        [Required]
        public DateTime LicenseExpirationDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
