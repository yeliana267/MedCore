

namespace MedCore.Application.Dtos.users.Doctors
{
    public class SaveDoctorsDto : DoctorsDto
    {

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
