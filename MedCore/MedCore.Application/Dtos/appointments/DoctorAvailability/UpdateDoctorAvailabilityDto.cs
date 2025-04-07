

namespace MedCore.Application.Dtos.appointments.DoctorAvailability
{
    public class UpdateDoctorAvailabilityDto : DoctorAvailabilityDto
    {
        public int AvailabilityID { get; set; }
        public int? DoctorID { get; set; }
        public DateOnly? AvailableDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
