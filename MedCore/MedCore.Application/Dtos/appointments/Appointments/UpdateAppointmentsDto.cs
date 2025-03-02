namespace MedCore.Application.Dtos.appointments.Appointments
{
    public class UpdateAppointmentsDto : AppointmentsDto
    {
        public int AppointmentID { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AppointmentDate { get; set; }
    }
}
