

using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.appointments
{
    public class AppointmentsModel
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public int StatusID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
