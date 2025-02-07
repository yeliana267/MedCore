
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.medical
{
    public class MedicalRecords : BaseEntity 
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public DateTime DateOfVisit { get; set; }

    }
}
