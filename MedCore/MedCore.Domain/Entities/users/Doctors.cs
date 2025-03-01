

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MedCore.Domain.Entities.users
{    [Table("Doctors", Schema = "users")]
    public sealed class Doctors : Base.BaseEntity<int>
    {
        [Column("DoctorID")]
        [Key]
        public override int Id { get; set; }

         public short SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }

        public string? AvailabilityModeId { get; set; }
        public DateOnly LicenseExpirationDate { get; set; } 
    }
}
