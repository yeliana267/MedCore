

namespace MedCore.Application.Dtos.appointments.Appointments
{
    public class SaveAppointmentsDto : AppointmentsDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
