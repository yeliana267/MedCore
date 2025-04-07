using System;

namespace MedCore.Web.Models.users.Patients
{
    public class EditPatientsModel
    {
        public int PatientID { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
        public bool IsActive { get; set; }
    }
}
