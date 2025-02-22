namespace MedCore.Model.Models
{
    internal class MedicalRecordsModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime DateOfVisit { get; set; }

    }
}
