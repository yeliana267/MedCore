

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Entities.appointments
{
    public sealed class DoctorAvailability : Base.BaseEntity<int>
    {
        [Column("AvailabilityID")]
        [Key]
        public override int Id { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }
}
