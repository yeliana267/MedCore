

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.Users
{
    public class DoctorModel
    {
        public int DoctorID { get; set; }

        [Required]
        public short SpecialtyID { get; set; }

        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        public int YearsOfExperience { get; set; }

        public string Education { get; set; }

        [MaxLength(1000)]
        public string? Bio { get; set; }

        public decimal? ConsultationFee { get; set; }

        [MaxLength(255)]
        public string? ClinicAddress { get; set; }

        public short? AvailabilityModeId { get; set; }

        public DateTime LicenseExpirationDate { get; set; }

        public int UserId { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
        public object SpecialtyName { get; set; }
    }
}
