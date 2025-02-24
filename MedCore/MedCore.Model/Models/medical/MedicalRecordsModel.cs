using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.medical
{
    public class MedicalRecordsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Diagnosis { get; set; }

        [Required]
        [MaxLength(500)]
        public string Treatment { get; set; }

        [Required]
        public DateTime DateOfVisit { get; set; }

    }
}
