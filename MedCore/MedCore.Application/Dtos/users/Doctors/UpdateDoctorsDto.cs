

using MedCore.Domain.Entities.users;

namespace MedCore.Application.Dtos.users.Doctors
{
    public class UpdateDoctorsDto : DoctorsDto
    {
        public int DoctorID { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
