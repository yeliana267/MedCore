

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Entities.appointments
{
    [Table("DoctorAvailability", Schema = "appointments")]

    public sealed class DoctorAvailability
    {
        [Column("AvailabilityID")]
        [Key]
        public  int AvailabilityID { get; set; }
        public int DoctorID { get; set; }   
        public DateOnly AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
