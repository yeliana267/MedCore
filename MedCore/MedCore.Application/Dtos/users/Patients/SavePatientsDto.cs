

namespace MedCore.Application.Dtos.users.Patients
{
    public class SavePatientsDto : PatientsDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
