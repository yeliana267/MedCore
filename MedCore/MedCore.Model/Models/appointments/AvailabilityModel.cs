

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.appointments
{
    public class AvailabilityModel
    {
        [Key]
        public int AvailabilityID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime AvailableDate { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan EndTime { get; set; }

      
    }
}
