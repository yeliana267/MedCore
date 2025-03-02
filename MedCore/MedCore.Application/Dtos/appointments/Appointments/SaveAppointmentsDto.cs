

namespace MedCore.Application.Dtos.appointments.Appointments
{
    public class SaveAppointmentsDto : DtoBase
    {
        public int PatientID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AppointmentDate { get; set; }

    }
}
