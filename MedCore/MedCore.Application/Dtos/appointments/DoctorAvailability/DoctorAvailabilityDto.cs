

namespace MedCore.Application.Dtos.appointments.DoctorAvailability
{
    public class DoctorAvailabilityDto : DtoBase
    {
        public DateOnly AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
  
    }
}
