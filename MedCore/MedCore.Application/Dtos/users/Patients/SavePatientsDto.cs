

namespace MedCore.Application.Dtos.users.Patients
{
    public class SavePatientsDto : PatientsDto
    {
        public int UserID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
