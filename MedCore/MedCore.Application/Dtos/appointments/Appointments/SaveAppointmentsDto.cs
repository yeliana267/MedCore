

namespace MedCore.Application.Dtos.appointments.Appointments
{
    public class SaveAppointmentsDto : AppointmentsDto
    {
        public int PatientID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AppointmentDate { get; set; }

    }
}
