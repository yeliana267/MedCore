
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Entities.users
{
    [Table("Patients", Schema = "users")]
    public sealed class Patients : Base.BaseEntity<int>
    {
        [Column("PatientsID")]
        [Key]
        public override int Id { get; set; }

        public DateOnly DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }

    }
}
