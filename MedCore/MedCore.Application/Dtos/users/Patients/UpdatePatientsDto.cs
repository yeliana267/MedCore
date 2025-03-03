

namespace MedCore.Application.Dtos.users.Patients
{
    public class UpdatePatientsDto : PatientsDto
    {
        public int PatientID { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
