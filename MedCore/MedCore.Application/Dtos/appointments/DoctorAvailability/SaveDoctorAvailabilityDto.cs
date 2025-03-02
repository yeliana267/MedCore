

namespace MedCore.Application.Dtos.appointments.DoctorAvailability
{
    public class SaveDoctorAvailabilityDto : DoctorAvailabilityDto
    {

        public int DoctorID { get; set; }
        public int AvailabilityID { get; set; }
    }
}
