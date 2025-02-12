
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.Users
{
    public sealed class UsersPatients : Users<int>
    {
        [Column("PatientID")]
        [Key]
        public override int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public int EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
       
    }
}
