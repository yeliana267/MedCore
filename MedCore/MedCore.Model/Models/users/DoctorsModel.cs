

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.users
{
    public class DoctorsModel
    {
        [Key]
        public int DoctorID { get; set; }
        public short SpecialtyID { get; set; }
        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public int YearsOfExperience { get; set; }
        [Required]
        public string Education { get; set; }
        public string? Bio { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? ConsultationFee { get; set; }
        [MaxLength(255)]
        public string? ClinicAddress { get; set; }
        public short? AvailabilityModeId { get; set; }
        [Required]
        public DateTime LicenseExpirationDate
        {
            get; set;
        }
    }
}
