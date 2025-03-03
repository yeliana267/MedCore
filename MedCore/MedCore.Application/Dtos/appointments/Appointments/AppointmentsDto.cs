namespace MedCore.Application.Dtos.appointments.Appointments
{
    public class AppointmentsDto : DtoBase
    {
        public int DoctorID { get; set; }
        public int StatusID { get; set; }
        public DateTime AppointmentDate { get; set; }

    }
}
