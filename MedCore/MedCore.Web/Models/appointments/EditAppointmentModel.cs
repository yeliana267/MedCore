using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.Models.appointments
{
    public class EditAppointmentModel
    {
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "El paciente es obligatorio.")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "El doctor es obligatorio.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha para la cita.")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }
    
    }
}
