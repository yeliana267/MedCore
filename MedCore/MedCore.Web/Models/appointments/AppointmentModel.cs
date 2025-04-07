using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.Models.appointments
{
    public class AppointmentModel
    {
        public int Id { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int StatusID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

