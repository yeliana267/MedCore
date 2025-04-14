namespace MedCore.Web.Models.doctorAvailability
{
    public class CreateDoctorAvailabilityModel
    {
        public int DoctorID { get; set; }
        public DateOnly AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }
}
