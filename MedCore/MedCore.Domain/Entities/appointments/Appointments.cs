
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Entities.appointments
{
    [Table("Appointments", Schema = "appointments")]
    public sealed class Appointments : Base.BaseEntity<int>
    {
        [Column("AppointmentID")]
        [Key]
        public override int Id { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int StatusID { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
