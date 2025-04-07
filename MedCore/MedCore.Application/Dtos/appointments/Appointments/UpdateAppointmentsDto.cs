namespace MedCore.Application.Dtos.appointments.Appointments
{
    public class UpdateAppointmentsDto :AppointmentsDto
    {
        public int AppointmentID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? StatusID { get; set; }
        public int? PatientID { get; set; }
        public int? DoctorID { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}   