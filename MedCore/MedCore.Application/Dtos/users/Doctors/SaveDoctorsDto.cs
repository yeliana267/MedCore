

namespace MedCore.Application.Dtos.users.Doctors
{
    public class SaveDoctorsDto : DoctorsDto
    {
        public int UserID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
