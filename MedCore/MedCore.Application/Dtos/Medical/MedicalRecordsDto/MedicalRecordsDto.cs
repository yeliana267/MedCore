
namespace MedCore.Application.Dtos.Medical.MedicalRecordsDto
{
    public class MedicalRecordsDto : DtoBase
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public required string Diagnosis { get; set; }
        public required string Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
