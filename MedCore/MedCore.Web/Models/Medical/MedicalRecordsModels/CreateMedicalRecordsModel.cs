namespace MedCore.Web.Models.Medical.MedicalRecordsModels
{
    public class CreateMedicalRecordsModel
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }
    }
}